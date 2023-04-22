using CoL.DB.Entities;
using CoL.Service.Importer;
using League = ClashOfLogs.Shared.League;
using Member = ClashOfLogs.Shared.Member;

namespace CoL.Service.Mappers;

internal class MemberMapper : IMapper<DBMember, Member>
{
    private readonly EntityProviderBase<DBLeague, int, League> leagueProvider;

    public MemberMapper(EntityProviderBase<DBLeague, int, League> leagueProvider)
    {
        this.leagueProvider = leagueProvider;
    }

    public DBMember CreateEntity(Member entity, DateTime timeStamp) =>
        new() { Tag = entity.Tag, CreatedAt = timeStamp };

    public async ValueTask UpdateEntityAsync(DBMember entity, Member model, DateTime timeStamp)
    {
        // todo add history
        if (!string.IsNullOrWhiteSpace(entity.Name))
            entity.History.Add(new HistoryEvent(timeStamp, nameof(entity.Name), model.Name, entity.Name));
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

            entity.History.Add(new HistoryEvent(timeStamp, nameof(entity.Donations), "0",
                entity.Donations.ToString()));
            entity.History.Add(new HistoryEvent(timeStamp, nameof(entity.DonationsReceived), "0",
                entity.DonationsReceived.ToString()));
        }

        entity.Donations = model.Donations;
        entity.DonationsReceived = model.DonationsReceived;


        entity.League = await leagueProvider.GetOrCreateAsync(model.League, timeStamp);

        entity.UpdatedAt = timeStamp;
    }
}