using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    public class BadgeUrls
    {
        [JsonPropertyName("small")]
        public string Small { get; set; }

        [JsonPropertyName("large")]
        public string Large { get; set; }

        [JsonPropertyName("medium")]
        public string Medium { get; set; }
    }
}