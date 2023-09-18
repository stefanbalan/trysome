using System;
using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace ClashOfLogs.Shared;

public record WarDetail
{
    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("teamSize")]
    public int TeamSize { get; set; }

    [JsonPropertyName("attacksPerMember")]
    public int AttacksPerMember { get; set; }

    [JsonPropertyName("preparationStartTime")]
    [JsonConverter(typeof(CustomDateTimeJsonConverter))]
    public DateTime? PreparationStartTime { get; set; }

    [JsonPropertyName("startTime")]
    [JsonConverter(typeof(CustomDateTimeJsonConverter))]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("endTime")]
    [JsonConverter(typeof(CustomDateTimeJsonConverter))]
    public DateTime? EndTime { get; set; }

    [JsonPropertyName("clan")]
    public WarClan Clan { get; set; }

    [JsonPropertyName("opponent")]
    public WarClan Opponent { get; set; }
}
