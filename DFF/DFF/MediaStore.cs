using DuplicateFileFind;

using Microsoft.EntityFrameworkCore;

namespace DFF;

public class MediaStore(
    ILogger<MediaStore> logger,
    IConfig config,
    DffContext context,
    BasicMetadataBuilder metadataBuilder,
    SourceFactory sourceFactory)
{
    private readonly string extensionFilter = config.ExtensionFilter;

    public async Task<JobResult> AddFolderToMediaStore(string path, bool presorted = false, bool keepSource = false)
    {
        var result = new JobResult();
        var source = sourceFactory.GetSourceFor(path);
        if (source is null)
        {
            result.Error = $"No source for path {path}";
            logger.LogError("No source for path {Path}", path);
            return result;
        }

        var extensions = extensionFilter.Split(',');

        var files = source.GetFiles();
        result.FileResults = new List<JobResult.FileResult>(files.Count);
        foreach (var file in files)
        {
            // filter by extension
            var extension = Path.GetExtension(file.Name);
            if (Array.Exists(extensions, ext => extension.Equals(ext, StringComparison.OrdinalIgnoreCase)))
            {
                result.FileResults.Add(new JobResult.FileResult(file.Name, JobResult.Result.Filtered));
                continue;
            }

            // add to media store if not exists
            try
            {
                // get basic metadata: file size, creation date, name
                var (creationDate, nameExtra) = await metadataBuilder.ComputeCreationDateAsync(file.Name);
                var hash128 = await metadataBuilder.ComputeHash128Async(file);

                // get possible matches
                var possibleMatches = await context.Files
                    //.Where(f => f.Length == file.Length) // same size
                    //.Where(f => f.CreationDateTime >= date.Date && f.CreationDateTime < date.Date.AddDays(1)) // same day
                    .Where(f => f.Hash128 != null && f.Hash128.Equals(hash128)
                                ||
                                (f.Length == file.Length &&
                                 f.OriginalFileDate <= file.CreationTime &&
                                 f.OriginalFileDate.AddDays(1) <= file.CreationTime)
                    )
                    .ToListAsync();

                if (possibleMatches.Count > 0)
                {
                    //reject file
                    logger.LogInformation("{File} exists in database", file.FullName);
                    SourceFileAction(result, file, true, keepSource);
                }
                else
                {
                    var destPath = ComputeDestinationPath(file.DirectoryName, creationDate, presorted);
                    var destName = ComputeDestinationName(file.Name, creationDate, nameExtra, presorted);

                    // physically copy the file
                    var destFullPathName = Path.Combine(destPath, destName);
                    try
                    {
                        if (File.Exists(destFullPathName))
                        {
                            logger.LogError("File {FullName} is not found in the database but destination file already exists (skipping) {DestFullPath}", file.FullName, destFullPathName);
                            result.FileResults.Add(new JobResult.FileResult(file.FullName, JobResult.Result.Failed));
                            continue; // skip to next file
                        }

                        file.CopyTo(destFullPathName);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    var folder = context
                                     .Folders
                                     .FirstOrDefault(f => f.Path.Equals(destPath, StringComparison.InvariantCultureIgnoreCase))
                                 ?? new IndexedFolder { Path = destPath };

                    var item = new IndexedFile {
                        OriginalName = file.Name,
                        OriginalPath = file.DirectoryName,
                        OriginalFileDate = file.CreationTime,
                        Length = file.Length,

                        Date = creationDate.HasValue ? DateOnly.FromDateTime(creationDate.Value.Date) : null,
                        Time = creationDate?.TimeOfDay,
                        Name = destName,
                        Folder = folder
                    };

                    //

                    logger.LogInformation("{File} is new", file.FullName);
                    SourceFileAction(result, file, false, keepSource);

                    context.Files.Add(item);
                }
            }
            catch (Exception e)
            {
                logger.LogError("Exception occured while processing {File} : {Message}", file.FullName, e.Message);
            }
        }

        return null;
    }

    private string ComputeDestinationName(string fileName, DateTime? creationDate, string nameExtra, bool presorted)
    {
        if (creationDate is null || presorted)
            return fileName;
        var ext = Path.GetExtension(fileName);
        var extra = nameExtra.Replace("_", " ").Trim();
        extra = string.IsNullOrWhiteSpace(extra) ? string.Empty : $" {extra}";
        return creationDate.Value.ToString("yyyyMMdd_HHmm") + $"{extra}.{ext}";
    }

    private readonly char pathSeparator = Path.PathSeparator;
    private const string UnknownPath = "_unknown";

    private string ComputeDestinationPath(string? originalPath, DateTime? creationDate, bool presorted)
    {
        if ((presorted || creationDate is null) && originalPath is not null)
        {
            var path = originalPath.Split(pathSeparator);
            return path[^1];
        }

        return creationDate.HasValue
            ? creationDate.Value.ToString($"yyyy{pathSeparator}MM")
            : UnknownPath;
    }

    private void SourceFileAction(JobResult result, FileInfo file, bool existing, bool keepSource)
    {
        if (keepSource)
        {
            logger.LogInformation("{NewOrExisting} {File} NOT deleted", existing ? "existing" : "new", file.FullName);
            result.FileResults.Add(new JobResult.FileResult(file.FullName,
                existing ? JobResult.Result.ExistingKept : JobResult.Result.NewKept));
        }

        try
        {
            file.Delete();
            logger.LogInformation("{NewOrExisting} {File} deleted", existing ? "existing" : "new", file.FullName);
            result.FileResults.Add(new JobResult.FileResult(file.FullName,
                existing ? JobResult.Result.ExistingRemoved : JobResult.Result.NewRemoved));
        }
        catch (Exception e)
        {
            logger.LogError("{File} could NOT be deleted: {Message}", file.FullName, e.Message);
            result.FileResults.Add(new JobResult.FileResult(file.FullName,
                existing ? JobResult.Result.ExistedNotRemoved : JobResult.Result.NewNotRemoved));
        }
    }
}

public class SourceFactory(IServiceProvider serviceProvider)
{
    public ISource? GetSourceFor(string path)
    {
        var logger = serviceProvider.GetService<ILogger<FileSource>>();
        return new FileSource(logger, path);
    }
}

public interface ISource
{
    IList<FileInfo> GetFiles();
}