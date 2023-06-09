using CoL.DB.Entities;
using CoL.Service.Importer;
using League = ClashOfLogs.Shared.League;
using Member = ClashOfLogs.Shared.Member;

namespace CoL.Service.Mappers;

public class MemberMapper : BaseMapper<DBMember, Member>
{
    private readonly EntityImporter<DBLeague, League> leagueProvider;

    public MemberMapper(EntityImporter<DBLeague, League> leagueProvider)
    {
        this.leagueProvider = leagueProvider;

        MapT2ToT1(m => m.Tag, dm => dm.Tag);
        MapT2ToT1(m => m.Name, dm => dm.Name,
            (m, dm) => dm.History.Add(
                new HistoryEvent(TimeStamp, nameof(dm.Name), m.Name, dm.Name)));
        MapT2ToT1(m => m.Role, dm => dm.Role,
            (m, dm) => dm.History.Add(
                new HistoryEvent(TimeStamp, nameof(dm.Role), m.Role, dm.Role)));
        MapT2ToT1(m => m.ExpLevel, dm => dm.ExpLevel);
        MapT2ToT1(m => m.Trophies, dm => dm.Trophies);
        MapT2ToT1(m => m.VersusTrophies, dm => dm.VersusTrophies);
        MapT2ToT1(m => m.ClanRank, dm => dm.ClanRank);
        MapT2ToT1(m => m.PreviousClanRank, dm => dm.PreviousClanRank);
        MapT2ToT1(m => m.Donations, dm => dm.Donations);
        MapT2ToT1(m => m.DonationsReceived, dm => dm.DonationsReceived);
    }

    public DateTime TimeStamp { get; set; }

    public override DBMember CreateAndUpdateEntity(Member model, DateTime timeStamp)
    {
        TimeStamp = timeStamp;

        var entity = base.CreateAndUpdateEntity(model, timeStamp);
        entity.League = leagueProvider.ImportAsync(model.League, timeStamp)
            .GetAwaiter().GetResult();
        return entity;
    }

    public override bool UpdateEntity(DBMember entity, Member model, DateTime timeStamp)
    {
        TimeStamp = timeStamp;
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

        entity.League = leagueProvider.ImportAsync(model.League, timeStamp)
            .GetAwaiter().GetResult();

        return base.UpdateEntity(entity, model, timeStamp);
    }
}