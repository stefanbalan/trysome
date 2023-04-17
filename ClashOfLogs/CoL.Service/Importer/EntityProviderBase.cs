using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;

namespace CoL.Service.Importer
{
    internal abstract class EntityProviderBase<TDbEntity, TKey, TEntity>
        where TDbEntity : BaseEntity
    {
        private readonly IRepository<TDbEntity, TKey> repository;
        private readonly IMapper<TDbEntity, TEntity> mapper;


        protected abstract TKey EntityKey(TEntity entity);

        protected EntityProviderBase(IRepository<TDbEntity, TKey> repository,
            IMapper<TDbEntity, TEntity> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async virtual Task<TDbEntity> GetOrCreateAsync(TEntity entity, DateTime timestamp)
        {
            var dbEntity = await repository.GetByIdAsync(EntityKey(entity));
            if (dbEntity == null)
            {
                dbEntity = mapper.CreateEntity(entity, timestamp);
                await repository.Add(dbEntity);
            }

            await mapper.UpdateEntityAsync(dbEntity, entity, timestamp);
            return dbEntity;
        }
    }
}