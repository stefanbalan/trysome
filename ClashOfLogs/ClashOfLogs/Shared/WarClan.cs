using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record WarClan
{
    [JsonPropertyName("tag")]
    public string Tag { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("badgeUrls")]
    public BadgeUrls BadgeUrls { get; set; }

    [JsonPropertyName("clanLevel")]
    public int ClanLevel { get; set; }

    [JsonPropertyName("attacks")]
    public int Attacks { get; set; }

    [JsonPropertyName("stars")]
    public int Stars { get; set; }

    [JsonPropertyName("destructionPercentage")]
    public double DestructionPercentage { get; set; }

    [JsonPropertyName("members")]
    public List<WarMember> Members { get; } = new List<WarMember>();
}