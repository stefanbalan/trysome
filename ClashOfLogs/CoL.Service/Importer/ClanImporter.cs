using System.Linq;
using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;
using Clan = ClashOfLogs.Shared.Clan;
using League = ClashOfLogs.Shared.League;
using Member = ClashOfLogs.Shared.Member;


namespace CoL.Service.Importer;

internal class ClanImporter : EntityImporter<DBClan, Clan>
{
    private readonly EntityImporter<DBLeague, League> leagueImporter;
    private readonly EntityImporter<DBMember, Member> memberImporter;

    public ClanImporter(
        IMapper<DBClan, Clan> mapper,
        IRepository<DBClan> repository,
        ILogger<EntityImporter<DBClan, Clan>> logger,
        EntityImporter<DBLeague, League> leagueImporter,
        EntityImporter<DBMember, Member> memberImporter)
        : base(mapper, repository, logger)
    {
        PersistChangesAfterImport = true;
        this.leagueImporter = leagueImporter;
        this.memberImporter = memberImporter;
    }

    protected override object?[] EntityKey(Clan entity) => new object?[] { entity.Tag };

    protected async override Task UpdateChildrenAsync(DBClan dbEntity, Clan clan, DateTime timeStamp)
    {
        // import leagues
        foreach (var member in clan.Members)
        {
            if (member.League == null) continue;
            await leagueImporter.ImportAsync(member.League, timeStamp);
        }

        // import members
        const string trueStr = "true";
        const string falseStr = "false";

        var previousMembers = dbEntity.Members.ToList();
        foreach (var member in clan.Members)
        {
            var dbMember = await memberImporter.ImportAsync(member, timeStamp);
            if (dbMember == null) continue;

            var alreadyMember = previousMembers.FirstOrDefault(m => m.Tag.Equals(dbMember.Tag));
            if (alreadyMember == null)
            {
                dbEntity.Members.Add(dbMember);
                dbMember.History.Add(new HistoryEvent(timeStamp, nameof(dbMember.IsMember), trueStr, falseStr));
                dbMember.IsMember = true;
            }
            else
                previousMembers.Remove(alreadyMember);
        }

        foreach (var pm in previousMembers)
        {
            pm.LastLeft = timeStamp;
            pm.IsMember = false;
            pm.History.Add(new HistoryEvent(timeStamp, nameof(pm.IsMember), falseStr, trueStr));
        }
    }
}