using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClashOfLogs.Shared
{
    public class Clan
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("badgeUrls")]
        public ImageUrls BadgeUrls { get; set; }

        [JsonProperty("clanLevel")]
        public int ClanLevel { get; set; }

        [JsonProperty("clanPoints")]
        public int ClanPoints { get; set; }

        [JsonProperty("clanVersusPoints")]
        public int ClanVersusPoints { get; set; }

        [JsonProperty("requiredTrophies")]
        public int RequiredTrophies { get; set; }

        [JsonProperty("warFrequency")]
        public string WarFrequency { get; set; }

        [JsonProperty("warWinStreak")]
        public int WarWinStreak { get; set; }

        [JsonProperty("warWins")]
        public int WarWins { get; set; }

        [JsonProperty("warTies")]
        public int WarTies { get; set; }

        [JsonProperty("warLosses")]
        public int WarLosses { get; set; }

        [JsonProperty("isWarLogPublic")]
        public bool IsWarLogPublic { get; set; }

        [JsonProperty("warLeague")]
        public WarLeague WarLeague { get; set; }

        [JsonProperty("members")]
        public int Members { get; set; }

        [JsonProperty("memberList")]
        public List<Member> MemberList { get; set; }

        [JsonProperty("labels")]
        public List<Label> Labels { get; set; }

        [JsonProperty("requiredVersusTrophies")]
        public int RequiredVersusTrophies { get; set; }

        [JsonProperty("requiredTownhallLevel")]
        public int RequiredTownhallLevel { get; set; }
    }
}