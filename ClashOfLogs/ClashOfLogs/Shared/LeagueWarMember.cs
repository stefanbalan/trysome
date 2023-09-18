using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record LeagueWarMember
{
    
    [JsonPropertyName("tag")]
    public string Tag { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("townHallLevel")]
    public int? TownHallLevel { get; init; }

  
}