using System.Collections;
using DuplicateFileFind;

namespace DFF;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private IFileSource filesource;
    private IndexDatabase db;
    private Destination destination;

    public Worker(ILogger<Worker> logger, IFileSource filesource, Destination destination)
    {
        this.logger = logger;
        this.filesource = filesource;
        this.destination = destination;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //await foreach (var file in filesource.GetFilesAsync().WithCancellation(stoppingToken))
        // { }

        foreach (var file in filesource.GetFiles())
        {
            try
            {
                logger.LogInformation("{File} started", file.FileInfo.FullName);
                if (!MatchesFilter(file))
                {
                    logger.LogInformation("{File} skipped, does not match filter", file.FileInfo.FullName);
                    continue;
                }

                if (db.HasFile(file))
                {
                    logger.LogInformation("{File} exists in database", file.FileInfo.FullName);
                    if (destination.ExistingAction(file))
                    {
                        // log? could be redundant
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
                logger.LogError("Exception occured while processing {File} : {Message}", file.FileInfo.FullName, e.Message);
            }
        }
    }

    private bool MatchesFilter(Item file) => true;
}

public class Destination
{
    public bool ExistingAction(Item file)
    {
        throw new NotImplementedException();
    }

    public bool NonExistingAction(Item file)
    {
        throw new NotImplementedException();
    }
}

public class IndexDatabase
{
    public IndexDatabase(DffContext context)
    {
        context.Database.EnsureCreated();
    }

    public bool HasFile(Item file)
    {
        throw new NotImplementedException();
    }

    public void AddFile(Item file)
    {
        throw new NotImplementedException();
    }
}