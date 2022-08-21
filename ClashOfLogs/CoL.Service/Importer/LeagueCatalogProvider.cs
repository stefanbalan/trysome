using ClashOfLogs.Shared;

using CoL.Service.Mappers;

namespace CoL.Service.Importer
{
    internal class LeagueCatalogProvider : EntityProviderBase<DBLeague, int, League>
    {
        public LeagueCatalogProvider(IRepository<DBLeague, int> repository, IMapper<DBLeague, League> mapper) : base(repository, mapper)
        {
        }


        public override async Task<DBLeague> GetOrCreateAsync(League league)
        {
            var dbLeague = await repository.GetByIdAsync(league.Id);
            if (dbLeague is null)
            {
                dbLeague = mapper.CreateEntity(league, DateTime.Now);
            }
            await mapper.UpdateEntityAsync(dbLeague, league, DateTime.Now);
            return dbLeague;
        }
    }


    internal class MemberProvider : EntityProviderBase<DBMember, string, Member>
    {
        public MemberProvider(IRepository<DBMember, string> repository, IMapper<DBMember, Member> mapper) : base(repository, mapper)
        {
        }

        public override Task<DBMember> GetOrCreateAsync(Member entity)
        {
            throw new NotImplementedException();
        }
    }


    internal abstract class EntityProviderBase<TEntity, TKey, TModel> where TEntity : DB.Entities.BaseEntity
    {
        protected readonly IRepository<TEntity, TKey> repository;
        protected readonly IMapper<TEntity, TModel> mapper;

        public EntityProviderBase(IRepository<TEntity, TKey> repository, IMapper<TEntity, TModel> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public abstract Task<TEntity> GetOrCreateAsync(TModel entity);
    }
}
