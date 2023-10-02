using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using ClashOfLogs.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

internal class FileJsonDataProvider : IJsonDataProvider
{
    private readonly DirectoryInfo directory;
    private readonly ILogger<FileJsonDataProvider> logger;
    private readonly JsonBackup jsonBackup;
    private DirectoryInfo? currentImportDir;

    public FileJsonDataProvider(IConfiguration config, ILogger<FileJsonDataProvider> logger,
        JsonBackup jsonBackup)
    {
        var importPath = config.GetValue(typeof(string), "JSONDirectory", ".") as string;
        if (string.IsNullOrWhiteSpace(importPath))
            throw new Exception("Invalid configuration, \"JSONDirectory\" is empty");
        directory = new DirectoryInfo(importPath);
        if (!directory.Exists) throw new Exception($"Invalid configuration, \"JSONDirectory\"=\"{directory.FullName}\" does not exist");
        this.logger = logger;
        this.jsonBackup = jsonBackup;
    }

    private bool HasImportData()
    {
        logger.LogInformation("Checking for import data 1");
        if (!directory.Exists) return false;
        DirectoryInfo? importDir = null;
        try
        {
            importDir = directory.EnumerateDirectories()
                .Where(d => !d.Name.Contains("imported"))
                .FirstOrDefault(d => IsCorrectDateTime(d.Name, out _));
        }
        catch (Exception ex)
        {
            logger.LogError("Cannot read import directory {ExMessage}", ex.Message);
        }

        return importDir != null;
    }

    private bool IsCorrectDateTime(string dirName, out DateTime date) => DateTime.TryParseExact(
        dirName,
        "yyyyMMdd HHmm",
        CultureInfo.InvariantCulture,
        DateTimeStyles.AssumeLocal, out date);

    public async Task<JsonData?> GetImportDataAsync()
    {
        if (!directory.Exists) return null;
        try
        {
            DateTime date = default;
            currentImportDir = directory.EnumerateDirectories()
                .Where(d => !d.Name.Contains("imported"))
                .Where(d => !d.Name.Contains("error"))
                .FirstOrDefault(d => IsCorrectDateTime(d.Name, out date));

            if (currentImportDir is null) return null;
            var result = new JsonData { Date = date };

            result.Clan = await ImportFileAsync<Clan>(currentImportDir, date, "clan");
            result.Warlog = await ImportFileAsync<Warlog>(currentImportDir, date, "warlog");
            result.CurrentWar = await ImportFileAsync<WarDetail>(currentImportDir, date, "currentwar");

            currentImportDir.MoveTo($"{currentImportDir.FullName}_imported");
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError("Error reading json files from disk {Message}", ex.Message);
            return null;
        }
    }

    public TimeSpan GetNextImportDelay() => HasImportData() ? TimeSpan.FromSeconds(5) : TimeSpan.FromHours(1);

    private async Task<T?> ImportFileAsync<T>(DirectoryInfo dir, DateTime date, string name)
    {
        var fileInfo = dir.EnumerateFiles($"{name}*.json").FirstOrDefault();
        if (fileInfo == null) return default;
        try
        {
            using var reader = fileInfo.OpenText();
            var json = await reader.ReadToEndAsync();
            await jsonBackup.BackupJsonAsync(json, name, date);
            reader.Close();
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