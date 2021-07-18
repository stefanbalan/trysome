using Newtonsoft.Json;

namespace ClashOfLogs.Shared
{
    public class League
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iconUrls")]
        public ImageUrls IconUrls { get; set; }
    }
}