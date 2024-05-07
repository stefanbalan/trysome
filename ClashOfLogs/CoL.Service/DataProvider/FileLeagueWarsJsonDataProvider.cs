﻿using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using ClashOfLogs.Shared;
using CoL.Service.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

/// <summary>
/// Imports json files with the new naming scheme [ddMMyyyy HHmm]_[type].json
/// </summary>
internal class FileLeagueWarsJsonDataProvider : IJsonDataProvider
{
    private readonly DirectoryInfo directory;
    private readonly ILogger<FileLeagueWarsJsonDataProvider> logger;
    private readonly JsonBackup jsonBackup;

    public FileLeagueWarsJsonDataProvider(IConfiguration config, ILogger<FileLeagueWarsJsonDataProvider> logger,
        JsonBackup jsonBackup)
    {
        var importPath = config.GetValue(typeof(string), "JSONDirectory", ".") as string;
        if (string.IsNullOrWhiteSpace(importPath))
            throw new MissingConfigurationException("Invalid configuration, import directory is empty");
        directory = new DirectoryInfo(importPath);
        if (!directory.Exists)
            throw new MissingConfigurationException($"Invalid configuration, import directory {directory.FullName} does not exist");
        this.logger = logger;
        this.jsonBackup = jsonBackup;
    }

    private bool HasImportData()
    {
        logger.LogInformation("Checking for import data 2");
        if (!directory.Exists)
        {
            logger.LogWarning("Import directory {Directory} does not exist", directory.FullName);
            return false;
        }

        FileInfo? fileInfo = null;
        try
        {
            fileInfo = directory.EnumerateFiles("*.json")
                .FirstOrDefault(d => IsJsonDataFile(d.Name, out _));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Cannot read import directory {ExMessage}", ex.Message);
        }

        return fileInfo != null;
    }

    private bool IsJsonDataFile(string fileName, out DateTime date)
    {
        var fn = fileName.Split('.');
        var fnp = fn[0].Split('_');
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
                fnp[0],
                "yyyyMMdd HHmm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal, out date);
        date = default;
        return false;
    }

    public async Task<JsonData?> GetImportDataAsync()
    {
        logger.LogInformation("Checking for import data 2");
        if (!directory.Exists)
        {
            logger.LogWarning("Import directory {Directory} does not exist", directory.FullName);
            return null;
        }
        try
        {
            var nextFile = GetNextFile(out var date);

            if (nextFile is null) return null;
            var result = new JsonData { Date = date };

            result.Clan = await ImportFileAsync<Clan>(directory, "clan", date);
            result.Warlog = await ImportFileAsync<Warlog>(directory, "warlog", date);
            result.CurrentWar = await ImportFileAsync<WarDetail>(directory, "currentwar", date);

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading json files from disk {Message}", ex.Message);
            return null;
        }
    }

    private FileInfo? GetNextFile(out DateTime date)
    {
        logger.LogInformation("Getting next file");
        DateTime d = default;

        var allFiles = directory.EnumerateFiles("???????? ????_*.json").ToList();
        logger.LogInformation("Found {Count} files", allFiles.Count);

        var firstOrDefault = allFiles.Find(f => IsJsonDataFile(f.Name, out d));
        logger.LogInformation("First file: {File}", firstOrDefault?.FullName);
        date = d;
        logger.LogInformation("Next file is {File} with date {Date}", firstOrDefault?.FullName, d);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading json file {Name}: {Exception}", name, ex.Message);
            return default;
        }
    }
}