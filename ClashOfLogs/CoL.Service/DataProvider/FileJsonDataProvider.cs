using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.IO;
using System.Linq;
using System.Text.Json;

namespace CoL.Service
{
    internal class FileJsonDataProvider : IJsonDataProvider
    {
        private readonly DirectoryInfo directory;
        private readonly IConfiguration config;
        private readonly ILogger<FileJsonDataProvider> logger;

        public FileJsonDataProvider(IConfiguration config, ILogger<FileJsonDataProvider> logger)
        {
            var importPath = config.GetValue(typeof(string), "JSONdirectory", ".") as string;
            if (string.IsNullOrWhiteSpace(importPath)) throw new Exception("Invalid configuration, import directory is empty");
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
                logger.LogError($"Cannot read import directory {ex.Message}");
            }

            return importDir != null;
        }

        public async Task<JsonData> GetImportDataAsync()
        {
            if (!(directory?.Exists ?? false)) return null;
            try
            {
                DateTime date = default;
                var importDir = directory.EnumerateDirectories()
                    .Where(d => !d.Name.Contains("imported"))
                    .FirstOrDefault(d => DateTime.TryParse(d.Name, out date));

                var result = new JsonData() { Date = date };

                result.Clan = await ImportFileAsync<ClashOfLogs.Shared.Clan>(importDir, "Clan");
                result.Warlog = await ImportFileAsync<ClashOfLogs.Shared.Warlog>(importDir, "Warlog");
                result.CurrentWar = await ImportFileAsync<ClashOfLogs.Shared.WarDetail>(importDir, "CurrentWar");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        private async Task<T> ImportFileAsync<T>(DirectoryInfo dir, string name)
        {
            var f = dir.EnumerateFiles($"{name}*.json").FirstOrDefault();
            if (f == null) return default;
            using var reader = f.Open(FileMode.Open);
            return await JsonSerializer.DeserializeAsync<T>(reader);
        }

    }
}
