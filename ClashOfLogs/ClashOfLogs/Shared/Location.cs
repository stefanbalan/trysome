using Newtonsoft.Json;

namespace ClashOfLogs.Shared
{
    public class Location
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isCountry")]
        public bool IsCountry { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}
