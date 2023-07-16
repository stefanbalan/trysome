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
    private readonly IValidator<TDbEntity>? validator;
    protected readonly IRepository<TDbEntity> Repository;
    private readonly IMapper<TDbEntity, TEntity> mapper;
    protected bool PersistChangesAfterImport;

    protected EntityImporter(
        IMapper<TDbEntity, TEntity> mapper,
        IRepository<TDbEntity> repository,
        ILogger<IEntityImporter<TDbEntity, TEntity>> logger,
        IValidator<TDbEntity>? validator = null)
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

            bool valid;
            if (dbEntity != null)
            {
                var changed = mapper.UpdateEntity(dbEntity, entity, timestamp);
                if (changed)
                {
                    if (validator != null && !validator.IsValid(dbEntity))
                    {
                        logger.LogInformation("Skipping existing {Type} : {Entity} : updates are not valid", typeof(TDbEntity).Name, EntityKey(entity));
                        return null;
                    }
                    Repository.Update(dbEntity);
                    logger.LogDebug("Updated existing {Type} : {Entity} ", typeof(TDbEntity).Name, EntityKey(entity));
                }
                else
                    logger.LogDebug("No update for existing {Type} : {Entity} ", typeof(TDbEntity).Name, EntityKey(entity));
            }
            else
            {
                dbEntity = mapper.CreateAndUpdateEntity(entity, timestamp);
                if (validator != null && !validator.IsValid(dbEntity))
                {
                    logger.LogInformation("Skipping adding {Type} : {Entity} : is not valid", typeof(TDbEntity).Name, EntityKey(entity));
                    return null;
                }
                try
                {
                    await Repository.AddAsync(dbEntity);
                    logger.LogDebug("Added new {Type} : {Entity}", typeof(TDbEntity).Name, EntityKey(entity));
                }
                catch (Exception ex)
                {
                    logger.LogError("Adding a new {Type} failed {Message}",
                        typeof(TDbEntity).Name,
                        ex.Message);
                    return null;
                }
            }

            await UpdateChildrenAsync(dbEntity, entity, timestamp);

            if (PersistChangesAfterImport || persist) try
                {
                    await Repository.PersistChangesAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError("Importing {Type} with key {Key} error: {Error}",
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
            logger.LogError("Importing {Type} with key {Key} error: {Error}",
                typeof(TDbEntity).Name,
                EntityKey(entity),
                ex.Message);
            return null;
        }
    }

    public abstract object?[] EntityKey(TEntity entity);

    public abstract Task UpdateChildrenAsync(TDbEntity dbEntity, TEntity entity, DateTime timestamp);
}