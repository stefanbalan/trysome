using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

public class WarClanMapper
{
    public static DBWarClan GetWarClan(WarClan clan) => new()
    {
        Tag = clan.Tag,
        Name = clan.Name,
        ClanLevel = clan.ClanLevel,
        Attacks = clan.Attacks,
        Stars = clan.Stars,
        DestructionPercentage = clan.DestructionPercentage,
        BadgeUrls = new DBBadgeUrls
        {
            Small = clan.BadgeUrls.Small,
            Medium = clan.BadgeUrls.Medium,
            Large = clan.BadgeUrls.Large
        }
    };
}