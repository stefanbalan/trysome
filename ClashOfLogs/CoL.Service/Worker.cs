using System.Threading;
using ClashOfLogs.Shared;
using CoL.DB.mssql;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service
{
    public class Worker : BackgroundService
    {
        private readonly EntityImporter<DBClan, Clan, string> clanDataImporter;
        private readonly CoLContext context;
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IJsonDataProvider importDataProvider;
        private readonly ILogger<Worker> logger;

        public Worker(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<Worker> logger,
            CoLContext context,
            IJsonDataProvider importDataProvider,
            EntityImporter<DBClan, Clan, string> clanDataImporter)
        {
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.logger = logger;
            this.context = context;
            this.importDataProvider = importDataProvider;
            this.clanDataImporter = clanDataImporter;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dbOk = await context.Database.CanConnectAsync(stoppingToken);
            if (!dbOk)
            {
                logger.LogError("database not available");
                hostApplicationLifetime.StopApplication();
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (importDataProvider.HasImportData())
                {
                    var jsonData = await importDataProvider.GetImportDataAsync();

                    await clanDataImporter.ImportAsync(jsonData.Clan, jsonData.Date);
                }

                await Task.Delay(120_000, stoppingToken);
            }
        }


        // public override Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);

        // public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
    }
}