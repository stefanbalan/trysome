using ClashOfLogs.Shared;

using CoL.Service.Mappers;

namespace CoL.Service.Importer
{
    internal class LeagueCatalogProvider
    {
        private IRepository<DBLeague, int> repository;
        private IMapper<DB.Entities.League, League> mapper;

        public LeagueCatalogProvider(IRepository<DBLeague, int> repository, IMapper<DBLeague, League> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        public async Task<DBLeague> GetOrCreateAsync(League league)
        {
            var dbLeague = await repository.GetByIdAsync(league.Id);
            if (dbLeague is null)
            {
                dbLeague = mapper.CreateEntity(league, DateTime.Now);
                repository.Save(dbLeague);
            }
            return dbLeague;
        }
    }
}
