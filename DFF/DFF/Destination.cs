namespace DFF;

public class Destination(ILogger<Destination> logger, IConfig config, NamingProvider namingProvider)
{

    private readonly bool keepSource = config.KeepSource;
    private readonly string destinationPath = config.DestinationPath;


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