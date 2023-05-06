using System.Collections.Generic;
using System.Linq;
using ClashOfLogs.Shared;
using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;
using WarMember = ClashOfLogs.Shared.WarMember;

namespace CoL.Service.Importer;

internal class WarDetailImporter : EntityImporter<DBWar, WarDetail>
{
    private readonly EntityImporter<DBWarClanMember, WarMember> warMemberClanImporter;
    private readonly EntityImporter<DBWarOpponentMember, WarMember> warMemberopponentImporter;

    public WarDetailImporter(IMapper<DBWar, WarDetail> mapper,
        IRepository<DBWar> repository,
        ILogger<WarDetailImporter> logger,
        EntityImporter<WarClanMember, WarMember> warMemberClanImporter,
        EntityImporter<WarOpponentMember, WarMember> warMemberopponentImporter)
        : base(mapper, repository, logger)
    {
        this.warMemberClanImporter = warMemberClanImporter;
        this.warMemberopponentImporter = warMemberopponentImporter;
    }

    protected override object?[] EntityKey(WarDetail entity)
        => new object?[] { entity.EndTime, entity.Clan.Tag, entity.Opponent.Tag };

    protected async override Task UpdateChildrenAsync(DBWar dbEntity, WarDetail entity, DateTime timestamp)
    {
        dbEntity.ClanMembers ??= new List<DBWarClanMember>();
        foreach (var clanMember in entity.Clan.Members)
        {
            var wm = await warMemberClanImporter.ImportAsync(clanMember, timestamp);
            if (wm == null) continue;
            var existing = dbEntity.ClanMembers.FirstOrDefault(wmc => string.Equals(wmc.Tag, clanMember.Tag));
            if (existing != null && ReferenceEquals(wm, existing)) throw new Exception("I knew it!?!");
        }

        dbEntity.OpponentMembers ??= new List<DBWarOpponentMember>();
        foreach (var opponentMember in entity.Opponent.Members)
        {
            var wm = await warMemberopponentImporter.ImportAsync(opponentMember, timestamp);
            if (wm == null) continue;
        }
    }
}
