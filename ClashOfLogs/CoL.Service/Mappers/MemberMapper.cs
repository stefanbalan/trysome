using CoL.DB.Entities;
using CoL.Service.Importer;
using League = ClashOfLogs.Shared.League;
using Member = ClashOfLogs.Shared.Member;

namespace CoL.Service.Mappers;

internal class MemberMapper : BaseMapper<DBMember, Member>
{
    private readonly EntityImporter<DBLeague, League> leagueProvider;

    public MemberMapper(EntityImporter<DBLeague, League> leagueProvider)
    {
        this.leagueProvider = leagueProvider;
    }

    public override DBMember CreateEntity(Member entity, DateTime timeStamp) =>
        base.CreateEntity(entity, timeStamp) with
        {
            Tag = entity.Tag
        };

    public override bool UpdateEntity(DBMember entity, Member model, DateTime timeStamp)
    {
        base.UpdateEntity(entity, model, timeStamp);

        if (!string.Equals(entity.Name, model.Name, StringComparison.InvariantCulture))
            entity.History.Add(new HistoryEvent(timeStamp, nameof(entity.Name), model.Name, entity.Name));
        entity.Name = model.Name;

        if (!string.Equals(entity.Role, model.Role, StringComparison.InvariantCulture))
            entity.History.Add(new HistoryEvent(timeStamp, nameof(entity.Role), model.Role, entity.Role));
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

            entity.History.Add(new HistoryEvent(timeStamp, nameof(entity.Donations), "0",
                entity.Donations.ToString()));
            entity.History.Add(new HistoryEvent(timeStamp, nameof(entity.DonationsReceived), "0",
                entity.DonationsReceived.ToString()));
        }

        entity.Donations = model.Donations;
        entity.DonationsReceived = model.DonationsReceived;

        // todo this is async code but method is sync
        entity.League = leagueProvider.ImportAsync(model.League, timeStamp)
            .GetAwaiter().GetResult();

        return true;
    }
}