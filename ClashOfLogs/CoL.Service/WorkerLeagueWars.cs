using System.Threading;
using ClashOfLogs.Shared;
using CoL.Service.DataProvider;
using CoL.Service.Importers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service;

public class WorkerLeagueWars : BackgroundService
{
    private readonly CoLContext context;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly IJsonDataProvider importDataProvider;
    private readonly ILogger<Worker> logger;


    public WorkerLeagueWars(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger,
        CoLContext context,
        ApiJsonLeagueWarsProvider importDataProvider)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.logger = logger;
        this.context = context;
        this.importDataProvider = importDataProvider;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Importing new data at: {Time}", DateTimeOffset.Now);

            JsonData? jsonData;
            var delay = TimeSpan.FromHours(4);

            if ((jsonData = await importDataProvider.GetImportDataAsync()) is not null)
            {
                logger.LogInformation("Import finished");

                delay = importDataProvider.GetNextImportDelay();

                if (jsonData.CurrentWar is not null)
                    logger.LogInformation("Current war ends at {WarEnd}", jsonData.CurrentWar.EndTime);
            }

            logger.LogInformation("Next import in {Delay}", delay);
            await Task.Delay(delay, stoppingToken);
        }
    }

    // kept for reference of what is possible 
    //// public override Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);

    // public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);
}