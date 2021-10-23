using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ClashOfLogs.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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


                await ImportFiles();

                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task ImportFiles()
        {
            if (!Directory.Exists(jsondirectory))
            {
                Directory.CreateDirectory(jsondirectory);
                var d = new DirectoryInfo(jsondirectory);
            }

            var jsonfiles = Directory.EnumerateFiles(jsondirectory, "*.json");
            var jf = jsonfiles.FirstOrDefault();
            if (jf is null) return;

            if (jf.Contains("clan")) await ImportClan(jf);


        }

        private async Task ImportClan(string jf)
        {
            if (!File.Exists(jf)) return;
            var stream = File.OpenRead(jf);
            var clan = await JsonSerializer.DeserializeAsync(stream, typeof(Clan));


        }
    }
}
