using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

public class JsonBackup
{
    private readonly string backupDirectoryPath;
    private readonly ILogger<JsonBackup> logger;

    public JsonBackup(string backupDirectoryPath, ILogger<JsonBackup> logger)
    {
        this.backupDirectoryPath = backupDirectoryPath;
        this.logger = logger;
    }

    public async Task BackupAsync(JsonData jsonData)
    {
        DirectoryInfo backupDir;
        try
        {
            backupDir = Directory.CreateDirectory(Path.Combine(backupDirectoryPath,
                jsonData.Date.ToString("yyyyMMdd HHmm")));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while creating backup directory");
            return;
        }

        if (jsonData.Clan != null) await BackupFileAsync(backupDir, "clan", jsonData.Clan);
        if (jsonData.Warlog != null) await BackupFileAsync(backupDir, "warlog", jsonData.Warlog);
        if (jsonData.CurrentWar != null) await BackupFileAsync(backupDir, "currentwar", jsonData.CurrentWar);
    }

    private async ValueTask BackupFileAsync(DirectoryInfo backupDir, string fileName, object jsonData)
    {
        // serialize the object to json and write it to a file
        var json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions {
            MaxDepth = 8,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var file = Path.Combine(backupDir.FullName, $"{fileName}.json");
        logger.LogInformation("Writing backup file {File}", file);
        await File.WriteAllTextAsync(file, json);
    }


    public async ValueTask BackupJsonAsync(string fileName, string json)
    {
        if (!Directory.Exists(backupDirectoryPath))
            try
            {
                Directory.CreateDirectory(backupDirectoryPath);
            }
            catch (Exception e)
            {
                logger.LogError(e, "error while creating backup directory");
            }
        var datestring = DateTime.Now.ToString("yyyyMMdd HHmm");
        var filename = Path.Combine(backupDirectoryPath, $"{datestring}_{fileName}.json");
        logger.LogInformation("Writing backup file {File}", filename);
        await File.WriteAllTextAsync(filename, json);
    }
}