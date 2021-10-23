﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    public class Clan
    {
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("badgeUrls")]
        public BadgeUrls BadgeUrls { get; set; }

        [JsonPropertyName("clanLevel")]
        public int ClanLevel { get; set; }

        [JsonPropertyName("clanPoints")]
        public int ClanPoints { get; set; }

        [JsonPropertyName("clanVersusPoints")]
        public int ClanVersusPoints { get; set; }

        [JsonPropertyName("requiredTrophies")]
        public int RequiredTrophies { get; set; }

        [JsonPropertyName("warFrequency")]
        public string WarFrequency { get; set; }

        [JsonPropertyName("warWinStreak")]
        public int WarWinStreak { get; set; }

        [JsonPropertyName("warWins")]
        public int WarWins { get; set; }

        [JsonPropertyName("warTies")]
        public int WarTies { get; set; }

        [JsonPropertyName("warLosses")]
        public int WarLosses { get; set; }

        [JsonPropertyName("isWarLogPublic")]
        public bool IsWarLogPublic { get; set; }

        [JsonPropertyName("warLeague")]
        public WarLeague WarLeague { get; set; }

        [JsonPropertyName("members")]
        public int Members { get; set; }

        [JsonPropertyName("memberList")]
        public List<ClanMember> MemberList { get; } = new List<ClanMember>();

        [JsonPropertyName("labels")]
        public List<Label> Labels { get; } = new List<Label>();

        [JsonPropertyName("requiredVersusTrophies")]
        public int RequiredVersusTrophies { get; set; }

        [JsonPropertyName("requiredTownhallLevel")]
        public int RequiredTownhallLevel { get; set; }
    }
}