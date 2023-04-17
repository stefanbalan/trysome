using ClashOfLogs.Shared;

namespace CoL.Service.Mappers
{
    internal class LeagueMapper : IMapper<DBLeague, League>
    {
        public DBLeague CreateEntity(League league, DateTime timeStamp)
        {
            var dbLeague = new DBLeague { Id = league.Id, };
            return dbLeague;
        }

        public Task UpdateEntityAsync(DBLeague dbLeague, League league, DateTime timeStamp)
        {
            dbLeague.Name = league.Name;

            dbLeague.IconUrls = new DB.Entities.IconUrls {
                Small = league.IconUrls.Small, Medium = league.IconUrls.Medium, Tiny = league.IconUrls.Tiny,
            };

            dbLeague.UpdatedAt = timeStamp;
            return Task.CompletedTask;
        }
    }
}