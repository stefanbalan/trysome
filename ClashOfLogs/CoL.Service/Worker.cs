using System.Threading;
using ClashOfLogs.Shared;
using CoL.DB;
using CoL.Service.DataProvider;
using CoL.Service.Importer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Clan = ClashOfLogs.Shared.Clan;

namespace CoL.Service;

public class Worker : BackgroundService
{
    private readonly EntityImporter<DBClan, Clan> clanDataImporter;
    private readonly EntityImporter<DBWar, WarSummary> warLogImporter;
    private readonly CoLContext context;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly IJsonDataProvider importDataProvider;
    private readonly ILogger<Worker> logger;

    public Worker(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger,
        CoLContext context,
        IJsonDataProvider importDataProvider,
        EntityImporter<DBClan, Clan> clanDataImporter,
        EntityImporter<DBWar, WarSummary> warLogImporter)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.logger = logger;
        this.context = context;
        this.importDataProvider = importDataProvider;
        this.clanDataImporter = clanDataImporter;
        this.warLogImporter = warLogImporter;
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

            JsonData? jsonData;
            if (importDataProvider.HasImportData()
                &&
                (jsonData = await importDataProvider.GetImportDataAsync()) is not null)
            {
                //todo test multiple import set and check updated properties
                var success = false;
                success &= jsonData.Clan is not null
                           && await clanDataImporter.ImportAsync(jsonData.Clan, jsonData.Date);
                if (jsonData.Warlog?.Items != null)
                    foreach (var warlogItem in jsonData.Warlog.Items)
                        success &= await warLogImporter.ImportAsync(warlogItem, jsonData.Date);

                // if (!importDataProvider.SetImported(success))
                // {
                //     logger.LogError("Failed to set data directory as processed, service will stop to avoid infinite loop");
                //     return;
                // }

                logger.LogInformation("Import result is {Success}", success);
            }

            await Task.Delay(10_000, stoppingToken);
        }
    }


    // public override Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);

    // public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
}