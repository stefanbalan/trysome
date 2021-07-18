using Newtonsoft.Json;

namespace ClashOfLogs.Shared
{
    public class Label
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iconUrls")]
        public ImageUrls IconUrls { get; set; }
    }
}