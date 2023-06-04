﻿using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;


// todo change the mapping with the new one, used in Lazy
// needs:
//      - only change "UPdatedAt" and only add to the DbSet as updated when there are actual changes
//      - validate that all (needed) properties are present, there are wars in log with null result or no name for opponent clan
public abstract class EntityImporter<TDbEntity, TEntity>
    where TDbEntity : BaseEntity
    where TEntity : class
{
    private readonly ILogger<EntityImporter<TDbEntity, TEntity>> logger;
    protected readonly IRepository<TDbEntity> Repository;
    private readonly IMapper<TDbEntity, TEntity> mapper;
    protected bool PersistChangesAfterImport;

    protected EntityImporter(
        IMapper<TDbEntity, TEntity> mapper,
        IRepository<TDbEntity> repository,
        ILogger<EntityImporter<TDbEntity, TEntity>> logger)
    {
        this.mapper = mapper;
        this.logger = logger;
        Repository = repository;
    }

    public async Task<TDbEntity?> ImportAsync(TEntity entity, DateTime timestamp, bool persist = false)
    {
        try
        {
            TDbEntity? dbEntity;
            try
            {
                dbEntity = await Repository.GetByIdAsync(EntityKey(entity));
            }
            catch (Exception ex)
            {
                logger.LogError("Getting {Type} by id failed {Message}",
                    typeof(TDbEntity).Name,
                    ex.Message);
                return null;
            }

            if (dbEntity != null)
            {
                if (mapper.UpdateEntity(dbEntity, entity, timestamp))
                {
                    Repository.Update(dbEntity);
                    logger.LogDebug("Updated existing {Type} : {Entity} ", typeof(TDbEntity).Name, EntityKey(entity));
                }
                else
                    logger.LogDebug("No update for existing {Type} : {Entity} ", typeof(TDbEntity).Name, EntityKey(entity));
            }
            else
            {
                dbEntity = mapper.CreateEntity(entity, timestamp);
                mapper.UpdateEntity(dbEntity, entity, timestamp);

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

    protected abstract object?[] EntityKey(TEntity entity);

    protected abstract Task UpdateChildrenAsync(TDbEntity dbEntity, TEntity entity, DateTime timestamp);
}