using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    public class IconUrls
    {
        [JsonPropertyName("small")]
        public string Small { get; set; }

        [JsonPropertyName("tiny")]
        public string Tiny { get; set; }

        [JsonPropertyName("medium")]
        public string Medium { get; set; }
    }
}