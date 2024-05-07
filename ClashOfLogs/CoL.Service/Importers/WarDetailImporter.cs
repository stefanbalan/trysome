using System.Collections.Generic;
using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;
using WarMember = ClashOfLogs.Shared.WarMember;

namespace CoL.Service.Importers;

public class WarDetailImporter : EntityImporter<DBWar, WarDetail>
{
    private readonly IEntityImporter<DBWarMember, WarMember> warMemberImporter;

    public WarDetailImporter(IMapper<DBWar, WarDetail> mapper,
        IRepository<DBWar> repository,
        ILogger<WarDetailImporter> logger,
        IEntityImporter<DBWarMember, WarMember> warMemberImporter)
        : base(mapper, repository, logger)
    {
        this.warMemberImporter = warMemberImporter;
    }

    public override object?[] EntityKey(WarDetail entity)
        => [entity.EndTime, entity.Clan.Tag, entity.Opponent.Tag];

    public async override Task UpdateChildrenAsync(DBWar dbEntity, WarDetail entity, DateTime timestamp)
    {
        dbEntity.ClanMembers ??= new List<DBWarMember>();
        foreach (var clanMember in entity.Clan.Members)
        {
            var wm = await warMemberImporter.ImportAsync(clanMember, timestamp);
            if (wm == null) continue;
            var existing = dbEntity.ClanMembers.Find(wmc => string.Equals(wmc.Tag, clanMember.Tag));
            if (existing != null && ReferenceEquals(wm, existing)) throw new Exception("I knew it!?!");
        }

        dbEntity.OpponentMembers ??= [];
        foreach (var opponentMember in entity.Opponent.Members)
        {
            var wm = await warMemberImporter.ImportAsync(opponentMember, timestamp);
        }
    }
}