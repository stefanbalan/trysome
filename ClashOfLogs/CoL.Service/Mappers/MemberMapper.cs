using ClashOfLogs.Shared;


namespace CoL.Service.Mappers
{
    internal class MemberMapper : IMapper<DBMember, Member>
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
