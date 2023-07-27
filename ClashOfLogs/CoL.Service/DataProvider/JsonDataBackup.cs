using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

public class JsonDataBackup
{
    private readonly string backupDirectoryPath;
    private readonly ILogger<JsonDataBackup> logger;

    public JsonDataBackup(string backupDirectoryPath, ILogger<JsonDataBackup> logger)
    {
        this.backupDirectoryPath = backupDirectoryPath;
        this.logger = logger;
    }

    public async Task BackupAsync(JsonData jsonData)
    {
        var backupDir = Directory.CreateDirectory(Path.Combine(backupDirectoryPath,
            jsonData.Date.ToString("yyyyMMdd HHmm")));
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
}