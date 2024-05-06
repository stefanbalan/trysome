using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

public class LeagueMapper : BaseMapper<DBLeague, League>
{
    public LeagueMapper()
    {
        MapT2ToT1(l => l.Id, dl => dl.Id);
        MapT2ToT1(l => l.Name, dl => dl.Name);
        MapT2ToT1(l => DbLeagueIconUrls(l.IconUrls), dl => dl.IconUrls);
    }

    private static DB.Entities.IconUrls DbLeagueIconUrls(IconUrls iconUrls) => new()
    {
        Small = iconUrls.Small,
        Medium = iconUrls.Medium,
        Tiny = iconUrls.Tiny,
    };
}