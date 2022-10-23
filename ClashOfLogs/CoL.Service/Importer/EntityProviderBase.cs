using CoL.Service.Mappers;
using CoL.Service.Repository;

namespace CoL.Service.Importer
{
    internal abstract class EntityProviderBase<TDbEntity, TKey, TEntity>
        where TDbEntity : DB.Entities.BaseEntity
    {
        protected readonly IRepository<TDbEntity, TKey> repository;
        protected readonly IMapper<TDbEntity, TEntity> mapper;


        public abstract TKey EntityKey(TEntity entity);

        public EntityProviderBase(IRepository<TDbEntity, TKey> repository, IMapper<TDbEntity, TEntity> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async virtual Task<TDbEntity> GetOrCreateAsync(TEntity entity, DateTime timestamp)
        {
            var dbEntity = await repository.GetByIdAsync(EntityKey(entity))
                           ?? mapper.CreateEntity(entity, timestamp);
            await mapper.UpdateEntityAsync(dbEntity, entity, timestamp);
            return dbEntity;
        }
    }
}