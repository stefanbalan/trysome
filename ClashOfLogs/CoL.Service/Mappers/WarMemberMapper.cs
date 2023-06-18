using System.Collections.Generic;
using ClashOfLogs.Shared;
using CoL.DB.Entities;
using Lazy.Util.EntityModelMapper;
using WarMember = ClashOfLogs.Shared.WarMember;

namespace CoL.Service.Mappers;

public class WarMemberMapper : BaseMapper<DBWarMember, WarMember>
{
    public WarMemberMapper()
    {
        MapT2ToT1(wm => wm.Tag, wm => wm.Tag);
        MapT2ToT1(wm => wm.Name, wm => wm.Name);
        MapT2ToT1(wm => wm.TownHallLevel, wm => wm.TownHallLevel);
        MapT2ToT1(wm => wm.MapPosition, wm => wm.MapPosition);
        MapT2ToT1(wm => BuildAttacks(wm.Attacks, 1), wm => wm.Attack1);
        MapT2ToT1(wm => BuildAttacks(wm.Attacks, 2), wm => wm.Attack2);
        MapT2ToT1(wm => wm.OpponentAttacks, wm => wm.OpponentAttacks);
        MapT2ToT1(wm => BuildAttack(wm.BestOpponentAttack), wm => wm.BestOpponentAttack);
    }

    private static WarAttack? BuildAttacks(List<Attack>? l, int number)
        => l?.Count >= number ? BuildAttack(l[number - 1]) : null;

    private static WarAttack BuildAttack(Attack a) =>
        new()
        {
            AttackerTag = a.AttackerTag,
            DefenderTag = a.DefenderTag,
            Stars = a.Stars,
            DestructionPercentage = a.DestructionPercentage,
            Order = a.Order,
            Duration = a.Duration
        };
}