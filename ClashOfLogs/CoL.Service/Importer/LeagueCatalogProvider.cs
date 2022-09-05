using ClashOfLogs.Shared;

using CoL.Service.Mappers;
using CoL.Service.Repository;

namespace CoL.Service.Importer
{
    internal class LeagueCatalogProvider : EntityProviderBase<DBLeague, int, League>
    {
        public LeagueCatalogProvider(IRepository<DBLeague, int> repository, IMapper<DBLeague, League> mapper) : base(repository, mapper)
        {
        }

        public override int EntityKey(League entity) => entity.Id;
    }


    internal class MemberProvider : EntityProviderBase<DBMember, string, Member>
    {
        public MemberProvider(IRepository<DBMember, string> repository, IMapper<DBMember, Member> mapper) : base(repository, mapper)
        {
        }

        public override string EntityKey(Member model) => model.Tag;
    }


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

        public virtual async Task<TDbEntity> GetOrCreateAsync(TEntity entity)
        {
            var dbEntity = await repository.GetByIdAsync(EntityKey(entity));
            if (dbEntity is null)
            {
                dbEntity = mapper.CreateEntity(entity, DateTime.Now);
            }
            await mapper.UpdateEntityAsync(dbEntity, entity, DateTime.Now);
            return dbEntity;
        }
    }
}
