using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    public class ClanMember
    {
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("expLevel")]
        public int ExpLevel { get; set; }

        [JsonPropertyName("league")]
        public League League { get; set; }

        [JsonPropertyName("trophies")]
        public int Trophies { get; set; }

        [JsonPropertyName("versusTrophies")]
        public int VersusTrophies { get; set; }

        [JsonPropertyName("clanRank")]
        public int ClanRank { get; set; }

        [JsonPropertyName("previousClanRank")]
        public int PreviousClanRank { get; set; }

        [JsonPropertyName("donations")]
        public int Donations { get; set; }

        [JsonPropertyName("donationsReceived")]
        public int DonationsReceived { get; set; }
    }
}