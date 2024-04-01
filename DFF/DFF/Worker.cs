namespace DFF;

public class Worker(ILogger<Worker> logger, FileSource fileSource, Destination destination)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach (var file in fileSource.GetFiles())
        {
            try
            {
                if (var item  = destination.HasItem(file))
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

        return Task.CompletedTask;
    }
}