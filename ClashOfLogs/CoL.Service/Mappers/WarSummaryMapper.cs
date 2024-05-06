using ClashOfLogs.Shared;
using WarClan = ClashOfLogs.Shared.WarClan;

namespace CoL.Service.Mappers;

public class WarSummaryMapper : BaseMapper<DBWar, WarSummary>
{
    public WarSummaryMapper()
    {
        //build mappings from WarSummary to DBWar
        MapT2ToT1(ws => WarClanMapper.GetWarClan(ws.Clan), dbw => dbw.Clan);
        MapT2ToT1(ws => WarClanMapper.GetWarClan(ws.Opponent), dbw => dbw.Opponent);
        MapT2ToT1(ws => ws.EndTime, dbw => dbw.EndTime);
        MapT2ToT1(ws => ws.Result, dbw => dbw.Result);
        MapT2ToT1(ws => ws.TeamSize, dbw => dbw.TeamSize);
        MapT2ToT1(ws => ws.AttacksPerMember, dbw => dbw.AttacksPerMember);
    }
}