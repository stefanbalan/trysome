using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using CoL.Service.Validators;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importers;

// needs:
//todo - validate that all (needed) properties are present, there are wars in log with null result or no name for opponent clan
//todo - check test coverage to see what properties are not being imported (eg: Clan.Location)

public interface IEntityImporter<TDbEntity, TEntity> where TDbEntity : BaseEntity
{
    Task<TDbEntity?> ImportAsync(TEntity entity, DateTime timestamp, bool persist = false);
    object?[] EntityKey(TEntity entity);
    Task UpdateChildrenAsync(TDbEntity dbEntity, TEntity entity, DateTime timestamp);
}

public abstract class EntityImporter<TDbEntity, TEntity>
    : IEntityImporter<TDbEntity, TEntity> where TDbEntity : BaseEntity
{
    private readonly ILogger<IEntityImporter<TDbEntity, TEntity>> logger;
    private readonly IValidator<TEntity>? validator;
    protected readonly IRepository<TDbEntity> Repository;
    private readonly IMapper<TDbEntity, TEntity> mapper;
    protected bool PersistChangesAfterImport;

    protected EntityImporter(
        IMapper<TDbEntity, TEntity> mapper,
        IRepository<TDbEntity> repository,
        ILogger<IEntityImporter<TDbEntity, TEntity>> logger,
        IValidator<TEntity>? validator = null)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.validator = validator;
        Repository = repository;
    }

    public async virtual Task<TDbEntity?> ImportAsync(TEntity entity, DateTime timestamp, bool persist = false)
    {
        try
        {
            if (validator != null && !validator.IsValid(entity))
            {
                logger.LogInformation("Skipping {Type} : {Entity} : entity is not valid", typeof(TEntity).Name,
                    EntityKey(entity));
                return null;
            }

            TDbEntity? dbEntity;
            try
            {
                dbEntity = await Repository.GetByIdAsync(EntityKey(entity));
            }
            catch (Exception e)
            {
                logger.LogError("Getting {Type} by id failed: {Message}", typeof(TDbEntity).Name, e.Message);
                return null;
            }

            if (dbEntity != null)
            {
                var changed = mapper.UpdateEntity(dbEntity, entity, timestamp);
                if (changed)
                {
                    Repository.Update(dbEntity);
                    logger.LogDebug("Updated existing {Type} : {Entity} ", typeof(TDbEntity).Name, EntityKey(entity));
                }
                else
                    logger.LogDebug("No update for existing {Type} : {Entity} ", typeof(TDbEntity).Name,
                        EntityKey(entity));
            }
            else
            {
                dbEntity = mapper.CreateAndUpdateEntity(entity, timestamp);
                try
                {
                    Repository.Add(dbEntity);
                    logger.LogDebug("Added new {Type} : {Entity}", typeof(TDbEntity).Name, EntityKey(entity));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "Adding a new {Type} failed {Message}",
                        typeof(TDbEntity).Name,
                        ex.Message);
                    return null;
                }
            }

            await UpdateChildrenAsync(dbEntity, entity, timestamp);

            if (PersistChangesAfterImport || persist)
                try
                {
                    await Repository.PersistChangesAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "Importing {Type} with key {Key} error: {Error}",
                        typeof(TDbEntity).Name,
                        EntityKey(entity),
                        ex.Message);
                    if (ex.InnerException != null)
                        logger.LogError("Inner exception: {Error}", ex.InnerException.Message);
                    return null;
                }

            return dbEntity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, 
                "Importing {Type} with key {Key} error: {Error}",
                typeof(TDbEntity).Name,
                EntityKey(entity),
                ex.Message);
            return null;
        }
    }

    public abstract object?[] EntityKey(TEntity entity);

    public abstract Task UpdateChildrenAsync(TDbEntity dbEntity, TEntity entity, DateTime timestamp);
}