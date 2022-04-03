using ClashOfLogs.Shared;


namespace CoL.Service.Mappers
{
    interface IMapper<TEntity, TModel> where TEntity : DB.Entities.BaseEntity
    {
        TEntity CreateEntity(TModel entity, DateTime timeStamp);
        void UpdateEntity(TEntity entity, TModel model, DateTime timeStamp);

    }

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

    class MemberMapper : IMapper<DBMember, Member>
    {
        public DBMember CreateEntity(Member entity, DateTime timeStamp)
        {
            return new DBMember
            {
                Tag = entity.Tag,
                CreatedAt = timeStamp
            };
        }

        public void UpdateEntity(DBMember entity, Member model, DateTime timeStamp)
        {
            entity.Name = model.Name;
            entity.Role = model.Role;
            entity.ExpLevel = model.ExpLevel;
            entity.Trophies = model.Trophies;
            entity.VersusTrophies = model.VersusTrophies;
            entity.ClanRank = model.ClanRank;
            entity.PreviousClanRank = model.PreviousClanRank;

            if (entity.Donations > model.Donations)
            {
                //new season
                entity.DonationsPreviousSeason = entity.Donations;
                entity.DonationsReceivedPreviousSeason = entity.DonationsReceived;
            }
            else
            {
                entity.Donations = model.Donations;
                entity.DonationsReceived = model.DonationsReceived;
            }

            entity.UpdatedAt = timeStamp;
        }
    }
}
