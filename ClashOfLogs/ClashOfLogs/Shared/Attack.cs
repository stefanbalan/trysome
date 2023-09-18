using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record Attack
{
    [JsonPropertyName("attackerTag")]
    public string AttackerTag { get; init; }

    [JsonPropertyName("defenderTag")]
    public string DefenderTag { get; init; }

    [JsonPropertyName("stars")]
    public int Stars { get; init; }

    [JsonPropertyName("destructionPercentage")]
    public int DestructionPercentage { get; init; }

    [JsonPropertyName("order")]
    public int Order { get; init; }

    [JsonPropertyName("duration")]
    public int Duration { get; init; }
}