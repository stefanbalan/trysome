using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    public class WarLeague
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}