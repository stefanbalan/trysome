using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record WarMember
{
    [JsonPropertyName("tag")]
    public string Tag { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("townhallLevel")]
    public int TownHallLevel { get; init; }

    [JsonPropertyName("mapPosition")]
    public int MapPosition { get; init; }

    [JsonPropertyName("attacks")]
    public List<Attack> Attacks { get; init; }

    [JsonPropertyName("opponentAttacks")]
    public int OpponentAttacks { get; init; }

    [JsonPropertyName("bestOpponentAttack")]
    public Attack BestOpponentAttack { get; init; }
}