using System.Collections.Generic;
using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace ClashOfLogs.Shared;

public record Attack
{
    [JsonPropertyName("attackerTag")]
    public string AttackerTag { get; set; }

    [JsonPropertyName("defenderTag")]
    public string DefenderTag { get; set; }

    [JsonPropertyName("stars")]
    public int Stars { get; set; }

    [JsonPropertyName("destructionPercentage")]
    public int DestructionPercentage { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }
}

public record WarMember
{
    [JsonPropertyName("tag")]
    public string Tag { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("townhallLevel")]
    public int TownhallLevel { get; set; }

    [JsonPropertyName("mapPosition")]
    public int MapPosition { get; set; }

    [JsonPropertyName("attacks")]
    public List<Attack> Attacks { get; set; }

    [JsonPropertyName("opponentAttacks")]
    public int OpponentAttacks { get; set; }

    [JsonPropertyName("bestOpponentAttack")]
    public Attack BestOpponentAttack { get; set; }
}

public record WarDetail
{
    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("teamSize")]
    public int TeamSize { get; set; }

    [JsonPropertyName("attacksPerMember")]
    public int AttacksPerMember { get; set; }

    [JsonPropertyName("preparationStartTime")]
    public string PreparationStartTime { get; set; }

    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public string EndTime { get; set; }

    [JsonPropertyName("clan")]
    public WarClan Clan { get; set; }

    [JsonPropertyName("opponent")]
    public WarClan Opponent { get; set; }
}
