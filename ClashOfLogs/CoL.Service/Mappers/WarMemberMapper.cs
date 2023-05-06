using ClashOfLogs.Shared;
using CoL.DB.Entities;
using WarMember = ClashOfLogs.Shared.WarMember;

namespace CoL.Service.Mappers;

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
