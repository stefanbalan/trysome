using System.Collections.Generic;

namespace CoL.DB.Entities;

public record LeagueWarSeason : BaseEntity
{
    public string State { get; set; }
    public string Season { get; set; }

    public List<LeagueWarClan>? Clans { get; set; }
    public List<LeagueWarRound> Rounds { get; set; }
}

public record LeagueWarRound
{
    public string[] WarTags { get; set; } = new string[4];
    public War[] Wars { get; set; } = new War[4];
}

public record LeagueWarClan
{
    public string Tag { get; set; } = null!;
    public string? Name { get; set; }
    public int ClanLevel { get; set; }
    public BadgeUrls? BadgeUrls { get; set; }
}