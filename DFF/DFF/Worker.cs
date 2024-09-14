namespace DFF;

public class Worker(ILogger<Worker> logger, FileSource fileSource, MediaStore mediaStore)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //foreach (var file in fileSource.GetFiles())
        //{
        //    try
        //    {
        //        var item = mediaStore.GetExistingItem(file);
        //        if (item is not null)
        //        {
        //            logger.LogInformation("{File} exists in database", file.FullName);
        //            if (mediaStore.ExistingAction(item))
        //            {
        //                // success
        //            }

        //            continue;
        //        }

        //        logger.LogInformation("{File} is new", file.FullName);
        //        if (mediaStore.NonExistingSourceFileAction(file))
        //        {
        //            db.AddFile(file);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogError("Exception occured while processing {File} : {Message}", file.FileInfo.FullName,
        //            e.Message);
        //    }
        //}

        return Task.CompletedTask;
    }
}