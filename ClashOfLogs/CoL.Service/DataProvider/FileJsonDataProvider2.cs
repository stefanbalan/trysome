using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using ClashOfLogs.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

internal class FileJsonDataProvider2 : IJsonDataProvider
{
    private readonly DirectoryInfo directory;
    private readonly ILogger<FileJsonDataProvider> logger;
    private readonly JsonBackup jsonBackup;

    public FileJsonDataProvider2(IConfiguration config, ILogger<FileJsonDataProvider> logger,
        JsonBackup jsonBackup)
    {
        var importPath = config.GetValue(typeof(string), "JSONDirectory", ".") as string;
        if (string.IsNullOrWhiteSpace(importPath))
            throw new Exception("Invalid configuration, import directory is empty");
        directory = new DirectoryInfo(importPath);
        if (!directory.Exists)
            throw new Exception($"Invalid configuration, import directory {directory.FullName} does not exist");
        this.logger = logger;
        this.jsonBackup = jsonBackup;
    }

    private bool HasImportData()
    {
        if (!directory.Exists) return false;
        DirectoryInfo? importDir = null;
        try
        {
            importDir = directory.EnumerateDirectories()
                .Where(d => !d.Name.Contains("imported"))
                .FirstOrDefault(d => IsJsonDataFile(d.Name, out _));
        }
        catch (Exception ex)
        {
            logger.LogError("Cannot read import directory {ExMessage}", ex.Message);
        }

        return importDir != null;
    }

    private bool IsJsonDataFile(string fileName, out DateTime date)
    {
        var fnp = fileName.Split('_');
        if (fnp.Length != 2)
        {
            date = default;
            return false;
        }

        if (string.Equals(fnp[1], "clan", StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(fnp[1], "currentwar", StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(fnp[1], "warlog", StringComparison.InvariantCultureIgnoreCase)
           )
            return DateTime.TryParseExact(
                fileName,
                "yyyyMMdd HHmm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal, out date);
        date = default;
        return false;
    }

    public async Task<JsonData?> GetImportDataAsync()
    {
        if (!directory.Exists) return null;
        try
        {
            DateTime date = default;
            var nextFile = GetNextFile(out date);

            if (nextFile is null) return null;
            var result = new JsonData { Date = date };

            result.Clan = await ImportFileAsync<Clan>(directory, "clan", date);
            result.Warlog = await ImportFileAsync<Warlog>(directory, "warlog", date);
            result.CurrentWar = await ImportFileAsync<WarDetail>(directory, "currentwar", date);

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError("Error reading json files from disk {Message}", ex.Message);
            return null;
        }
    }

    private FileInfo? GetNextFile(out DateTime date)
    {
        DateTime d = default;
        var firstOrDefault = directory.EnumerateFiles("???????? ????_*.json")
            .FirstOrDefault(f => IsJsonDataFile(f.Name, out d));
        date = d;
        return firstOrDefault;
    }


    public TimeSpan GetNextImportDelay() => HasImportData() ? TimeSpan.FromSeconds(5) : TimeSpan.FromHours(1);

    private async Task<T?> ImportFileAsync<T>(DirectoryInfo dir, string name, DateTime date)
    {
        var fileInfo = dir.EnumerateFiles($"{date:yyyyMMdd HHmm}_{name}.json").FirstOrDefault();
        if (fileInfo == null) return default;
        try
        {
            using var reader = fileInfo.OpenText();
            var json = await reader.ReadToEndAsync();
            await jsonBackup.BackupJsonAsync(json, name, date);
            fileInfo.Delete();
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception e)
        {
            logger.LogError("Error reading json file {Name}: {Exception}", name, e.Message);
            return default;
        }
    }
}