using ClashOfLogs.Shared;

using CoL.Service.Importer;

namespace CoL.Service.Mappers
{
    internal class MemberMapper : IMapper<DBMember, Member>
    {
        private readonly EntityProviderBase<DBLeague,int, League> leagueProvider;
        public MemberMapper(EntityProviderBase<DBLeague, int, League> leagueProvider)
        {
            this.leagueProvider = leagueProvider;
        }

        public DBMember CreateEntity(Member entity, DateTime timeStamp) =>
            new() {
                Tag = entity.Tag,
                CreatedAt = timeStamp
            };

        public async Task UpdateEntityAsync(DBMember entity, Member model, DateTime timeStamp)
        {
            entity.Name = model.Name;
            entity.Role = model.Role;
            entity.ExpLevel = model.ExpLevel;
            entity.Trophies = model.Trophies;
            entity.VersusTrophies = model.VersusTrophies;
            entity.ClanRank = model.ClanRank;
            entity.PreviousClanRank = model.PreviousClanRank;
            entity.IsMember = true;

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

            entity.League = await leagueProvider.GetOrCreateAsync(model.League, timeStamp);//todo cannot find table Leagues

            entity.UpdatedAt = timeStamp;
        }

    }
}
