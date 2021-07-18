using Newtonsoft.Json;

namespace ClashOfLogs.Shared
{
    public class ImageUrls
    {
        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }
    }
}