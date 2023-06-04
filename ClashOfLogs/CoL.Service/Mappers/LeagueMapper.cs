using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

internal class LeagueMapper : BaseMapper<DBLeague, League>
{
    public override DBLeague CreateEntity(League league, DateTime timeStamp) =>
        base.CreateEntity(league, timeStamp) with { Id = league.Id };

    public override bool UpdateEntity(DBLeague dbLeague, League league, DateTime timeStamp)
    {
        dbLeague.Name = league.Name;

        dbLeague.IconUrls = new DB.Entities.IconUrls
        {
            Small = league.IconUrls.Small,
            Medium = league.IconUrls.Medium,
            Tiny = league.IconUrls.Tiny,
        };

        return true;
    }
}