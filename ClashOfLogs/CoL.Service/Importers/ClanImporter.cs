using System.Linq;
using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;
using Clan = ClashOfLogs.Shared.Clan;
using League = ClashOfLogs.Shared.League;
using Member = ClashOfLogs.Shared.Member;


namespace CoL.Service.Importers;

public class ClanImporter : EntityImporter<DBClan, Clan>
{
    private readonly IEntityImporter<DBLeague, League> leagueImporter;
    private readonly IEntityImporter<DBMember, Member> memberImporter;

    public ClanImporter(
        IMapper<DBClan, Clan> mapper,
        IRepository<DBClan> repository,
        ILogger<ClanImporter> logger,
        IEntityImporter<DBLeague, League> leagueImporter,
        IEntityImporter<DBMember, Member> memberImporter)
        : base(mapper, repository, logger)
    {
        PersistChangesAfterImport = true;
        this.leagueImporter = leagueImporter;
        this.memberImporter = memberImporter;
    }

    public override object?[] EntityKey(Clan entity) => [entity.Tag];

    public async override Task UpdateChildrenAsync(DBClan dbEntity, Clan clan, DateTime timestamp)
    {
        // import leagues
        foreach (var league in clan.Members.Select(m => m.League).Where(l => l is not null)) 
            await leagueImporter.ImportAsync(league, timestamp);

        // import members
        const string trueStr = "true";
        const string falseStr = "false";

        var previousMembers = dbEntity.Members.ToList();
        foreach (var member in clan.Members)
        {
            var dbMember = await memberImporter.ImportAsync(member, timestamp);
            if (dbMember == null) continue;

            var alreadyMember = previousMembers.Find(m => m.Tag.Equals(dbMember.Tag));
            if (alreadyMember == null)
            {
                dbEntity.Members.Add(dbMember);
                dbMember.History.Add(new HistoryEvent(timestamp, nameof(dbMember.IsMember), trueStr, falseStr));
                dbMember.IsMember = true;
            }
            else
                previousMembers.Remove(alreadyMember);
        }

        foreach (var pm in previousMembers)
        {
            pm.LastLeft = timestamp;
            pm.IsMember = false;
            pm.History.Add(new HistoryEvent(timestamp, nameof(pm.IsMember), falseStr, trueStr));
        }
    }
}
