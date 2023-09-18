using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record BadgeUrls
{
    [JsonPropertyName("small")]
    public string Small { get; init; }

    [JsonPropertyName("large")]
    public string Large { get; init; }

    [JsonPropertyName("medium")]
    public string Medium { get; init; }
}