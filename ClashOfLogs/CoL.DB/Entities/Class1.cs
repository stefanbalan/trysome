using System.Text.Json.Serialization;

namespace CoL.DB.Entities
{
    public class WarSummary
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }

        [JsonPropertyName("teamSize")]
        public int TeamSize { get; set; }

        [JsonPropertyName("clan")]
        public WarClan Clan { get; set; }

        [JsonPropertyName("opponent")]
        public WarClan Opponent { get; set; }
    }
}
