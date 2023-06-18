using ClashOfLogs.Shared;
using WarClan = ClashOfLogs.Shared.WarClan;

namespace CoL.Service.Mappers;

public class WarDetailMapper : BaseMapper<DBWar, WarDetail>
{
    public WarDetailMapper()
    {
        MapT2ToT1(w => w.State, dw => dw.State);
        MapT2ToT1(w => w.TeamSize, dw => dw.TeamSize);
        MapT2ToT1(w => w.AttacksPerMember, dw => dw.AttacksPerMember);

        MapT2ToT1(w => w.PreparationStartTime, dw => dw.PreparationStartTime);
        MapT2ToT1(w => w.StartTime, dw => dw.StartTime);
        MapT2ToT1(w => w.EndTime, dw => dw.EndTime);

        // MapT2ToT1(w => WarClanMapper.GetWarClan(w.Clan), dw => dw.Clan);
        // MapT2ToT1(w => WarClanMapper.GetWarClan(w.Opponent), dw => dw.Opponent);
    }
}