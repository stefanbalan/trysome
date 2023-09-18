using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record LeagueWarGroup
{
    [JsonPropertyName("state")]
    public string State { get; init; }

    [JsonPropertyName("season")]
    public string Season { get; init; }

    [JsonPropertyName("clans")]
    public IReadOnlyList<LeagueWarClan> Clans { get; init; }

    [JsonPropertyName("rounds")]
    public IReadOnlyList<LeagueWarRound> Rounds { get; init; }
}