using CoL.DB;
using CoL.DB.Entities;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

public abstract class EntityImporter<TDbEntity, TEntity, TKey>
    where TDbEntity : BaseEntity
    where TEntity : class
{
    protected readonly CoLContext Context;
    private readonly ILogger<EntityImporter<TDbEntity, TEntity, TKey>> logger;

    protected EntityImporter(CoLContext context,
        ILogger<EntityImporter<TDbEntity, TEntity, TKey>> logger)
    {
        this.Context = context;
        this.logger = logger;
    }

    public async Task<bool> ImportAsync(TEntity entity, DateTime dateTime)
    {
        try
        {
            var dbEntity = await FindExistingAsync(entity) ?? await CreateNewAsync(entity, dateTime);
            UpdateProperties(dbEntity, entity, dateTime);
            await UpdateChildrenAsync(dbEntity, entity, dateTime);
            await Context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return false;
        }
    }

    protected abstract Task<TDbEntity?> FindExistingAsync(TEntity entity);
    protected abstract Task<TDbEntity> CreateNewAsync(TEntity entity, DateTime dateTime);
    protected abstract void UpdateProperties(TDbEntity tDbEntity, TEntity entity, DateTime dateTime);
    protected abstract Task UpdateChildrenAsync(TDbEntity tDbEntity, TEntity entity, DateTime dateTime);
}