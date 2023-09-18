using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record LeagueWarClan
{
    [JsonPropertyName("tag")]
    public string Tag { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("clanLevel")]
    public int? ClanLevel { get; init; }

    [JsonPropertyName("badgeUrls")]
    public BadgeUrls BadgeUrls { get; init; }

    [JsonPropertyName("members")]
    public IReadOnlyList<LeagueWarMember> Members { get; init; }
}