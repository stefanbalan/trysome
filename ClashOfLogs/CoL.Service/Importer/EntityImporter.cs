using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

public abstract class EntityImporter<TDbEntity, TEntity>
    where TDbEntity : BaseEntity
    where TEntity : class
{
    private readonly ILogger<EntityImporter<TDbEntity, TEntity>> logger;
    protected readonly IRepository<TDbEntity> Repository;
    private readonly IMapper<TDbEntity, TEntity> mapper;

    protected EntityImporter(
        IMapper<TDbEntity, TEntity> mapper,
        IRepository<TDbEntity> repository,
        ILogger<EntityImporter<TDbEntity, TEntity>> logger)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.Repository = repository;
    }

    public async Task<TDbEntity?> ImportAsync(TEntity entity, DateTime timestamp)
    {
        try
        {
            var dbEntity = await FindExistingAsync(entity) ?? await CreateNewAsync(entity, timestamp);
            await UpdateProperties(dbEntity, entity, timestamp);
            await UpdateChildrenAsync(dbEntity, entity, timestamp);

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

    protected virtual Task<TDbEntity?> FindExistingAsync(TEntity entity)
        => Repository.GetByIdAsync(EntityKey(entity));

    protected async virtual Task<TDbEntity> CreateNewAsync(TEntity entity, DateTime timestamp)
    {
        var dbEntity = mapper.CreateEntity(entity, timestamp);
        dbEntity.CreatedAt = timestamp;
        await Repository.Add(dbEntity);
        return dbEntity;
    }

    protected async virtual Task UpdateProperties(TDbEntity dbEntity, TEntity entity, DateTime timestamp)
    {
        await mapper.UpdateEntityAsync(dbEntity, entity, timestamp);
        dbEntity.UpdatedAt = timestamp;
    }

    protected abstract Task UpdateChildrenAsync(TDbEntity tDbEntity, TEntity entity, DateTime timestamp);
}