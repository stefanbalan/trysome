using System.Collections.Generic;
using System.Linq;
using ClashOfLogs.Shared;
using CoL.DB;
using CoL.DB.Entities;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using WarMember = ClashOfLogs.Shared.WarMember;

namespace CoL.Service.Importer;

internal class WarDetailImporter : EntityImporter<DBWar, WarDetail>
{
    private readonly EntityImporter<DBWarClanMember, WarMember> warMemberClanImporter;
    private readonly EntityImporter<DBWarOpponentMember, WarMember> warMemberopponentImporter;

    public WarDetailImporter(IMapper<DBWar, WarDetail> mapper,
        IRepository<DBWar> repository,
        ILogger<EntityImporter<DBWar, WarDetail>> logger,
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

internal class WarMemberClanImporter : EntityImporter<DBWarClanMember, WarMember>
{
    public WarMemberClanImporter(IMapper<DBWarClanMember, WarMember> mapper, IRepository<DBWarClanMember> repository,
        ILogger<EntityImporter<DBWarClanMember, WarMember>> logger) : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(WarMember entity) => new object?[] { entity.Tag };

    protected async override Task UpdateChildrenAsync(DBWarClanMember dbEntity, WarMember entity, DateTime timestamp)
        => await Task.CompletedTask;
}

internal class WarMemberOpponentImporter : EntityImporter<DBWarOpponentMember, WarMember>
{
    public WarMemberOpponentImporter(
        IMapper<DBWarOpponentMember, WarMember> mapper,
        IRepository<DBWarOpponentMember> repository,
        ILogger<EntityImporter<DBWarOpponentMember, WarMember>> logger) :
        base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(WarMember entity) => new object?[] { entity.Tag };

    protected async override Task UpdateChildrenAsync(DBWarOpponentMember dbEntity, WarMember entity,
        DateTime timestamp) => await Task.CompletedTask;
}

internal class WarMemberMapper : IMapper<DBWarMember, WarMember>
{
    public virtual DBWarMember CreateEntity(WarMember entity, DateTime timeStamp)
        => new() { Tag = entity.Tag };

    public virtual void UpdateEntity(DBWarMember entity, WarMember model, DateTime timeStamp)
    {
        entity.TownhallLevel = model.TownhallLevel;
        entity.MapPosition = model.MapPosition;

        if (model.Attacks is { Count: > 0 })
        {
            entity.Attack1 = BuildAttack(model.Attacks[0]);
            if (model.Attacks.Count > 1)
                entity.Attack2 = BuildAttack(model.Attacks[1]);
        }

        entity.OpponentAttacks = model.OpponentAttacks;

        if (model.BestOpponentAttack is not null)
            entity.BestOpponentAttack = BuildAttack(model.BestOpponentAttack);

        static WarAttack BuildAttack(Attack a) => new()
        {
            AttackerTag = a.AttackerTag,
            DefenderTag = a.DefenderTag,
            Stars = a.Stars,
            DestructionPercentage = a.DestructionPercentage,
            Order = a.Order,
            Duration = a.Duration
        };
    }
}

internal class WarClanMemberMapper : WarMemberMapper, IMapper<DBWarClanMember, WarMember>
{
    public override WarClanMember CreateEntity(WarMember entity, DateTime timeStamp)
        => (WarClanMember)base.CreateEntity(entity, timeStamp);

    public void UpdateEntity(WarClanMember entity, WarMember model, DateTime timeStamp)
        => base.UpdateEntity(entity, model, timeStamp);
}

internal class WarOpponentMemberMapper : WarMemberMapper, IMapper<DBWarOpponentMember, WarMember>
{
    public override WarOpponentMember CreateEntity(WarMember entity, DateTime timeStamp)
        => (WarOpponentMember)base.CreateEntity(entity, timeStamp);

    public void UpdateEntity(WarOpponentMember entity, WarMember model, DateTime timeStamp)
        => base.UpdateEntity(entity, model, timeStamp);
}

internal class WarClanMemberRepository : BaseEFRepository<CoLContext, DBWarClanMember>
{
    public WarClanMemberRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<WarClanMember> EntitySet => Context.Set<DBWarClanMember>();
}

internal class WarClanOpponentMemberRepository : BaseEFRepository<CoLContext, DBWarOpponentMember>
{
    public WarClanOpponentMemberRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBWarOpponentMember> EntitySet => Context.Set<DBWarOpponentMember>();
}