using Newtonsoft.Json;

namespace ClashOfLogs.Shared
{
    public class WarLeague
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}