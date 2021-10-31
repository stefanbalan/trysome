using ClashOfLogs.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CoL.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly string jsondirectory;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            jsondirectory = config["JSONdirectory"];
            if (string.IsNullOrEmpty(jsondirectory))
            {
                jsondirectory = "JSON";
                config["JSONdirectory"] = jsondirectory;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                await ImportDirectories();

                await Task.Delay(120_000, stoppingToken);
            }
        }

        private async Task ImportDirectories()
        {
            try
            {
                if (!Directory.Exists(jsondirectory))
                {
                    Directory.CreateDirectory(jsondirectory);
                    var d = new DirectoryInfo(jsondirectory);
                }

                var importdates = Directory.EnumerateDirectories(jsondirectory);
                foreach (var importdate in importdates)
                {
                    var dir = new DirectoryInfo(importdate);
                    if (!dir.Exists) continue;

                    var dirDateString = dir.Name;
                    if (!DateTime.TryParse(dirDateString, out var dirDate)) continue;

                    await ImportFiles(dir, dirDate);
                }

            }
            catch (Exception ex) { }
        }

        async Task ImportFiles(DirectoryInfo dir, DateTime date)
        {
            var jsonFiles = dir.EnumerateFiles("*.json");

            foreach (var jsonFile in jsonFiles)
            {
                if (!jsonFile.Exists) continue;
                if (jsonFile.Name.Contains("clan")) await ImportClan(jsonFile);
            }
        }

        private async Task ImportClan(FileInfo file)
        {

            if (!file.Exists) return;
            var stream = file.OpenRead();
            var clan = await JsonSerializer.DeserializeAsync(stream, typeof(Clan)  , new JsonSerializerOptions() {  });
            stream.Close();
        }
    }
}
