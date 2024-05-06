using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record WarLeague
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
