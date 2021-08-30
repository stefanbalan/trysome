using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    public class Location
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("isCountry")]
        public bool IsCountry { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
    }
}