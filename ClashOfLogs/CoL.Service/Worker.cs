using System.Threading;
using ClashOfLogs.Shared;
using CoL.DB;
using CoL.Service.DataProvider;
using CoL.Service.Importers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service;

public class Worker : BackgroundService
{
    private readonly IEntityImporter<DBLeague, League> leagueImporter;
    private readonly IEntityImporter<DBClan, Clan> clanDataImporter;
    private readonly IEntityImporter<DBWar, WarSummary> warLogImporter;
    private readonly IEntityImporter<DBWar, WarDetail> warDetailImporter;
    private readonly CoLContext context;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly IJsonDataProvider importDataProvider;
    private readonly JsonBackup jsonDataBackup;
    private readonly ILogger<Worker> logger;


    public Worker(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger,
        CoLContext context,
        IJsonDataProvider importDataProvider,
        JsonBackup jsonDataBackup,
        IEntityImporter<DBClan, Clan> clanDataImporter,
        IEntityImporter<DBWar, WarSummary> warLogImporter,
        IEntityImporter<DBWar, WarDetail> warDetailImporter,
        IEntityImporter<DBLeague, League> leagueImporter)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.logger = logger;
        this.context = context;
        this.importDataProvider = importDataProvider;
        this.jsonDataBackup = jsonDataBackup;
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
            await context.Database.EnsureCreatedAsync(stoppingToken);

            logger.LogInformation("Attempting to create database");
            dbOk = await context.Database.CanConnectAsync(stoppingToken);
            
            if (!dbOk)
            {
                logger.LogError("Database not available");
                hostApplicationLifetime.StopApplication();
                return;
            }
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Importing new data at: {Time}", DateTimeOffset.Now);

            JsonData? jsonData;
            var delay = TimeSpan.FromHours(4);

            if ((jsonData = await importDataProvider.GetImportDataAsync()) is not null)
            {
                if (jsonData.Clan is not null)
                    foreach (var clanMember in jsonData.Clan.Members)
                        _ = await leagueImporter.ImportAsync(clanMember.League, jsonData.Date);

                if (jsonData.Clan is not null)
                    _ = await clanDataImporter.ImportAsync(jsonData.Clan, jsonData.Date);

                if (jsonData.Warlog?.Items != null)
                    foreach (var warlogItem in jsonData.Warlog.Items)
                        _ = await warLogImporter.ImportAsync(warlogItem, jsonData.Date);

                if (jsonData.CurrentWar != null)
                {
                    var curentWar = await warDetailImporter.ImportAsync(jsonData.CurrentWar, jsonData.Date);
                }

                // if (!importDataProvider.SetImported(success))
                // {
                //     logger.LogError("Failed to set data directory as processed, service will stop to avoid infinite loop");
                //     return;
                // }

                await jsonDataBackup.BackupAsync(jsonData);

                logger.LogInformation("Import finished");

                delay = importDataProvider.GetNextImportDelay();
                    
                logger.LogInformation("Current war ends at {WarEnd}", jsonData.CurrentWar?.EndTime);
            }

            logger.LogInformation("Next import in {Delay}", delay);
            await Task.Delay(delay, stoppingToken);
        }
    }


    // public override Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);

    // public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
}