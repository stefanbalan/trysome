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
    private DirectoryInfo? currentImportDir;

    public FileJsonDataProvider(IConfiguration config, ILogger<FileJsonDataProvider> logger)
    {
        var importPath = config.GetValue(typeof(string), "JSONDirectory", ".") as string;
        if (string.IsNullOrWhiteSpace(importPath))
            throw new Exception("Invalid configuration, import directory is empty");
        directory = new DirectoryInfo(importPath);
        if (!directory.Exists) throw new Exception("Invalid configuration, import directory does not exist");
        this.logger = logger;
    }

    public bool HasImportData()
    {
        if (!directory.Exists) return false;
        DirectoryInfo? importDir = null;
        try
        {
            importDir = directory.EnumerateDirectories()
                .Where(d => !d.Name.Contains("imported"))
                .FirstOrDefault(d => DateTime.TryParse(d.Name, out _));
        }
        catch (Exception ex)
        {
            logger.LogError("Cannot read import directory {ExMessage}", ex.Message);
        }

        return importDir != null;
    }

    public async Task<JsonData?> GetImportDataAsync()
    {
        if (!directory.Exists) return null;
        try
        {
            DateTime date = default;
            currentImportDir = directory.EnumerateDirectories()
                .Where(d => !d.Name.Contains("imported"))
                .Where(d => !d.Name.Contains("error"))
                .FirstOrDefault(d => DateTime.TryParse(d.Name, out date));

            if (currentImportDir is null) return null;
            var result = new JsonData { Date = date };

            result.Clan = await ImportFileAsync<Clan>(currentImportDir, "Clan");
            result.Warlog = await ImportFileAsync<Warlog>(currentImportDir, "Warlog");
            result.CurrentWar = await ImportFileAsync<WarDetail>(currentImportDir, "CurrentWar");
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError("Error reading json files from disk {Message}", ex.Message);
            return null;
        }
    }

    private async Task<T?> ImportFileAsync<T>(DirectoryInfo dir, string name)
    {
        var fileInfo = dir.EnumerateFiles($"{name}*.json").FirstOrDefault();
        if (fileInfo == null) return default;
        try
        {
            await using var reader = fileInfo.Open(FileMode.Open);
            return await JsonSerializer.DeserializeAsync<T>(reader);
        }
        catch (Exception e)
        {
            logger.LogError("Error reading json file {Name}: {Exception}", name, e.Message);
            return default;
        }
    }
}