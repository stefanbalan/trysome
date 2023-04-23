using System.Linq;
using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class ClanImporter : EntityImporter<DBClan, Clan>
{
    private readonly EntityImporter<DBMember, Member> memberImporter;

    public ClanImporter(
        IMapper<DBClan, Clan> mapper,
        IRepository<DBClan> repository,
        ILogger<EntityImporter<DBClan, Clan>> logger,
        EntityImporter<DBMember, Member> memberImporter)
        : base(mapper, repository, logger)
    {
        this.memberImporter = memberImporter;
    }

    protected override object?[] EntityKey(Clan entity) => new object?[] { entity.Tag };

    protected async override Task UpdateChildrenAsync(DBClan dbClan, Clan clan, DateTime timeStamp)
    {
        var previousMembers = dbClan.Members.ToList();

        foreach (var member in clan.Members)
        {
            var dbMember = await memberImporter.ImportAsync(member, timeStamp);

            dbClan.Members.Contains(dbMember);

            previousMembers.RemoveAll(m => m.Tag.Equals(dbMember.Tag));
        }

        foreach (var pm in previousMembers)
        {
            pm.LastLeft = timeStamp;
            pm.IsMember = false;
        }
    }
}