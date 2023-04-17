using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;

namespace CoL.Service.Importer
{
    internal class LeagueCatalogProvider : EntityProviderBase<DBLeague, int, League>
    {
        public LeagueCatalogProvider(IRepository<DBLeague, int> repository, IMapper<DBLeague, League> mapper) : base(
            repository, mapper)
        {
        }

        protected override int EntityKey(League entity) => entity.Id;
    }
}