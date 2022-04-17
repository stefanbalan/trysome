using ClashOfLogs.Shared;


namespace CoL.Service.Mappers
{

    internal class ClanMapper : IMapper<DBClan, Clan>
    {
        public DBClan CreateEntity(Clan entity, DateTime timeStamp)
        {
            return new DBClan
            {
                Tag = entity.Tag,
                CreatedAt = timeStamp,
            };
        }

        public void UpdateEntity(DB.Entities.Clan entity, Clan model, DateTime timeStamp)
        {
            entity.Name = model.Name;
            entity.Type = model.Type;
            entity.Description = model.Description;
            entity.ClanLevel = model.ClanLevel;
            entity.ClanPoints = model.ClanPoints;
            entity.ClanVersusPoints = model.ClanVersusPoints;
            entity.RequiredTrophies = model.RequiredTrophies;
            entity.WarFrequency = model.WarFrequency;
            entity.WarWinStreak = model.WarWinStreak;
            entity.WarWins = model.WarWins;
            entity.WarTies = model.WarTies;
            entity.WarLosses = model.WarLosses;
            entity.IsWarLogPublic = model.IsWarLogPublic;
            entity.RequiredVersusTrophies = model.RequiredVersusTrophies;
            entity.RequiredTownhallLevel = model.RequiredTownhallLevel;

            entity.UpdatedAt = timeStamp;
        }
    }
}
