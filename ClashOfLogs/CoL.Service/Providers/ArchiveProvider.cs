using System.IO;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Providers;

public class ArchiveProvider : IArchiveProvider
{
    private readonly ILogger<ArchiveProvider> logger;
    private readonly string archivePath;
    private readonly DirectoryInfo archiveDir;

    public ArchiveProvider(string archivePath, ILogger<ArchiveProvider> logger)
    {
        this.archivePath = archivePath;
        this.logger = logger;
        archiveDir = new DirectoryInfo(archivePath);
    }

    public async Task<bool> ArchiveAsync(DateTime dateTime, string objectContent, string objectName, bool? success)
    {
        if (!archiveDir.Exists)
            try
            {
                archiveDir.Create();
            }
            catch (Exception e)
            {
                logger.LogError("Failed to create archive directory {ArchivePath}: {ExMessage}", archivePath,
                    e.Message);
                return false;
            }

        var setDirName = dateTime.ToString("yyyy-MM-dd HH:mm:ss" +
                                           (success.HasValue ? (success.Value ? "-success" : "-error") : ""));
        var setDir = new DirectoryInfo(Path.Combine(archivePath, setDirName));
        try
        {
            if (!setDir.Exists) setDir.Create();
            var filePath = Path.Combine(setDir.FullName, $"{objectName}.json");
            await File.WriteAllTextAsync(filePath, objectContent);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to archive {ObjectName} to {ArchivePath}: {ExMessage}", objectName, archivePath,
                ex.Message);
            return false;
        }
    }
}