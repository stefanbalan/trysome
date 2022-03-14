using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CoL.Service
{
    internal class FileDataProvider : IDataProvider
    {
        private readonly DirectoryInfo directory;
        private readonly ILogger log;

        public FileDataProvider(IConfiguration config, , ILogger<FileDataProvider> logger)
        {
          


            log = logger;

            var jsondirectory = config["JSONdirectory"];
            if (string.IsNullOrEmpty(jsondirectory))
            {
                jsondirectory = "JSON";
                config["JSONdirectory"] = jsondirectory;
            }
            directory = new DirectoryInfo(jsondirectory);
        }

        JsonImport IDataProvider.GetImport()
        {
            if (!(directory?.Exists ?? false)) return null;
            var result = new JsonImport();

            var nextDir = directory.EnumerateDirectories()
                .Where(d => DateTime.TryParse(d.Name, out var _))
                .FirstOrDefault(d => !d.Name.Contains("imported"));

            if (nextDir is null) return null;

            var files = nextDir.EnumerateFiles("*.json");

            result.Clan = ReadAndDeserialize<ClashOfLogs.Shared.Clan>(files);
            result.WarDetail = ReadAndDeserialize<ClashOfLogs.Shared.WarDetail>(files, "currentwar");
            result.WarLog = ReadAndDeserialize<ClashOfLogs.Shared.Warlog>(files);

            return result;

        }

        private T ReadAndDeserialize<T>(System.Collections.Generic.IEnumerable<FileInfo> files, string fileName = nameof(T))
        {
            var f = files.FirstOrDefault(f => f.Name.Contains(fileName, StringComparison.InvariantCultureIgnoreCase));
            string str = null;
            try
            {
                str = f.OpenText().ReadToEnd();
            }
            catch (Exception ex)
            {
                log.LogError($"Failed to read file {f.FullName}: {ex.Message}");
            }

            if (string.IsNullOrWhiteSpace(str)) return default;

            try
            {
                return JsonSerializer.Deserialize<T>(str);
            }
            catch (Exception ex)
            {
                log.LogError($"Failed to deserialize {nameof(T)}: {ex.Message}");
                return default;
            }
        }
    }
}
