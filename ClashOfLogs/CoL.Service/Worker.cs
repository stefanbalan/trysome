using System.Threading;
using ClashOfLogs.Shared;
using CoL.DB.mssql;
using CoL.Service.Importer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service
{
    public class Worker : BackgroundService
    {
        private readonly EntityImporter<DBClan, Clan, string> clanDataImporter;
        private readonly WarlogDataImporter warlogDataImporter;
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
                logger.LogInformation("Importing new data at: {Time}", DateTimeOffset.Now);

                if (importDataProvider.HasImportData())
                {
                    var jsonData = await importDataProvider.GetImportDataAsync();

                    //todo test multiple import set and check updated properties
                    var success = await clanDataImporter.ImportAsync(jsonData.Clan, jsonData.Date);
                    success &= await warlogDataImporter.ImportAsync(jsonData.Warlog, jsonData.Date);
                    if (!importDataProvider.SetImported(success))
                    {
                        logger.LogError(
                            "Failed to set data directory as processed, service will stop to avoid infinite loop");
                        return;
                    }
                }

                await Task.Delay(10_000, stoppingToken);
            }
        }


        // public override Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);

        // public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
    }
}
