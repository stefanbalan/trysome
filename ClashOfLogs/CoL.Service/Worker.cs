using System.Threading;
using ClashOfLogs.Shared;
using CoL.DB;
using CoL.Service.DataProvider;
using CoL.Service.Importer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service;

public class Worker : BackgroundService
{
    private readonly EntityImporter<DBLeague, League> leagueImporter;
    private readonly EntityImporter<DBClan, Clan> clanDataImporter;
    private readonly EntityImporter<DBWar, WarSummary> warLogImporter;
    private readonly EntityImporter<DBWar, WarDetail> warDetailImporter;
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
        EntityImporter<DBWar, WarSummary> warLogImporter,
        EntityImporter<DBWar, WarDetail> warDetailImporter,
        EntityImporter<DBLeague, League> leagueImporter)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.logger = logger;
        this.context = context;
        this.importDataProvider = importDataProvider;
        this.clanDataImporter = clanDataImporter;
        this.warLogImporter = warLogImporter;
        this.warDetailImporter = warDetailImporter;
        this.leagueImporter = leagueImporter;
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
                var success = true;

                if (jsonData.Clan is not null)
                    foreach (var clanMember in jsonData.Clan.Members)
                        success &= await leagueImporter.ImportAsync(clanMember.League, jsonData.Date) != null;

                if (jsonData.Clan is not null)
                    success &= await clanDataImporter.ImportAsync(jsonData.Clan, jsonData.Date) != null;

                if (jsonData.Warlog?.Items != null)
                    foreach (var warlogItem in jsonData.Warlog.Items)
                        success &= await warLogImporter.ImportAsync(warlogItem, jsonData.Date) != null;

                if (jsonData.CurrentWar != null)
                    success &= await warDetailImporter.ImportAsync(jsonData.CurrentWar, jsonData.Date) != null;

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