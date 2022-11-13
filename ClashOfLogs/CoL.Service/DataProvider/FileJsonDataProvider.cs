using System.IO;
using System.Linq;
using System.Text.Json;
using ClashOfLogs.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoL.Service
{
    internal class FileJsonDataProvider : IJsonDataProvider
    {
        private readonly DirectoryInfo directory;
        private readonly IConfiguration config;
        private readonly ILogger<FileJsonDataProvider> logger;
        private DirectoryInfo? currentImportDir;

        public FileJsonDataProvider(IConfiguration config, ILogger<FileJsonDataProvider> logger)
        {
            var importPath = config.GetValue(typeof(string), "JSONDirectory", ".") as string;
            if (string.IsNullOrWhiteSpace(importPath))
                throw new Exception("Invalid configuration, import directory is empty");
            directory = new DirectoryInfo(importPath);
            if (!directory.Exists) throw new Exception("Invalid configuration, import directory does not exist");
            this.config = config;
            this.logger = logger;
        }

        public bool HasImportData()
        {
            if (!(directory?.Exists ?? false)) return false;
            DirectoryInfo importDir = null;
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

        public async Task<JsonData> GetImportDataAsync()
        {
            if (!(directory?.Exists ?? false)) return null;
            try
            {
                DateTime date = default;
                currentImportDir = directory.EnumerateDirectories()
                    .Where(d => !d.Name.Contains("imported"))
                    .Where(d => !d.Name.Contains("error"))
                    .FirstOrDefault(d => DateTime.TryParse(d.Name, out date));

                var result = new JsonData { Date = date };

                result.Clan = await ImportFileAsync<Clan>(currentImportDir, "Clan");
                result.Warlog = await ImportFileAsync<Warlog>(currentImportDir, "Warlog");
                result.CurrentWar = await ImportFileAsync<WarDetail>(currentImportDir, "CurrentWar");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public bool SetImported(bool success)
        {
            if (currentImportDir is null) return false;

            try
            {
                currentImportDir.MoveTo(currentImportDir.Name + (success ? " imported" : " error"));
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to set data directory as processed {DirName}: {ExMessage}",
                    currentImportDir.Name, ex.Message);
                return false;
            }
        }

        private async Task<T> ImportFileAsync<T>(DirectoryInfo dir, string name)
        {
            var f = dir.EnumerateFiles($"{name}*.json").FirstOrDefault();
            if (f == null) return default;
            await using var reader = f.Open(FileMode.Open);
            return await JsonSerializer.DeserializeAsync<T>(reader);
        }
    }
}
