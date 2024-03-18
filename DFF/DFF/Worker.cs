using System.Collections;

namespace DFF;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly IFileSource fileSource;
    private IndexDatabase db;
    private readonly Destination destination;

    public Worker(ILogger<Worker> logger, IFileSource fileSource, Destination destination)
    {
        this.logger = logger;
        this.fileSource = fileSource;
        this.destination = destination;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // async version: await foreach (var file in fileSource.GetFilesAsync().WithCancellation(stoppingToken)) { }

        foreach (var file in fileSource.GetFiles())
        {
            try
            {
                logger.LogInformation("{File} started", file.FileInfo.FullName);
                if (!MatchesFilter(file))
                {
                    logger.LogInformation("{File} skipped, does not match filter", file.FileInfo.FullName);
                    continue;
                }


                //build an IndexedFile with metadata

                //write a IndexedFile comparer to determine if the file is new or not

                if (db.HasFile(file))
                {
                    logger.LogInformation("{File} exists in database", file.FileInfo.FullName);
                    if (destination.ExistingAction(file))
                    {
                        // success
                    }

                    continue;
                }

                logger.LogInformation("{File} is new", file.FileInfo.FullName);
                if (destination.NonExistingAction(file))
                {
                    db.AddFile(file);
                }
            }
            catch (Exception e)
            {
                logger.LogError("Exception occured while processing {File} : {Message}", file.FileInfo.FullName,
                    e.Message);
            }
        }
    }

    private bool MatchesFilter(Item file) => true;
}