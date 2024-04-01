using DuplicateFileFind;
using Microsoft.EntityFrameworkCore;

namespace DFF;

public class Destination(
    ILogger<Destination> logger,
    IConfig config,
    DffContext context,
    BasicMetadataBuilder metadataBuilder,
    NamingProvider namingProvider)
{
    private readonly bool keepSource = config.KeepSource;
    private readonly string destinationPath = config.DestinationPath;


    public async ValueTask<Item?> CreateNotExistingItemAsync(FileInfo file)
    {
        var (creationDate, nameExtra) = await metadataBuilder.ComputeCreationDateAsync(file);

        var possibleMatches = await context.Files
            .Where(f => f.Length == file.Length)
            .Where(f => f.CreationDateTime.Equals(creationDate))
            .ToListAsync();

        if (possibleMatches.Count > 0)
        {
            //check md5
            return null;
        }

        return new Item() {
            DateTime = creationDate ?? file.CreationTime,
            FileInfo = file,
            Name = creationDate.HasValue ? nameExtra : file.Name,
            Path = file.DirectoryName
        };
    }

    public bool ExistingAction(Item file)
    {
        if (keepSource)
        {
            logger.LogInformation("{File} NOT deleted", file.FileInfo.FullName);
            return true;
        }

        try
        {
            file.FileInfo.Delete();
            logger.LogInformation("{File} deleted", file.FileInfo.FullName);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError("{File} NOT deleted: {Message}", file.FileInfo.FullName, e.Message);
            return false;
        }
    }

    public bool NonExistingAction(Item file)
    {
        var relativePath = namingProvider.GetRelativePath(file);
        var destinationDir = Path.Combine(destinationPath, relativePath);

        if (keepSource)
        {
            file.FileInfo.CopyTo(Path.Combine(destinationDir, file.FileInfo.Name));
            logger.LogInformation("{File} copied", file.FileInfo.FullName);
            return true;
        }

        try
        {
            file.FileInfo.MoveTo(Path.Combine(destinationDir, file.FileInfo.Name));
            logger.LogInformation("{File} moved", file.FileInfo.FullName);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError("{File} NOT deleted: {Message}", file.FileInfo.FullName, e.Message);
            return false;
        }
    }
}