using ClashOfLogs.Shared;

namespace CoL.Service.Mappers
{
    class LeagueMapper : IMapper<DBLeague, League>
    {
        public DBLeague CreateEntity(League entity, DateTime timeStamp)
        {
            var league = new DB.Entities.League
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = timeStamp
            };
            return league;
        }

        public void UpdateEntity(DB.Entities.League entity, League model, DateTime timeStamp)
        {
            entity.Name = model.Name;

            entity.UpdatedAt = timeStamp;
        }
    }
}
