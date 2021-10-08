﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClashOfLogs.Shared
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

    public class Attack
    {
        [JsonPropertyName("attackerTag")]
        public string AttackerTag { get; set; }

        [JsonPropertyName("defenderTag")]
        public string DefenderTag { get; set; }

        [JsonPropertyName("stars")]
        public int Stars { get; set; }

        [JsonPropertyName("destructionPercentage")]
        public int DestructionPercentage { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }

    public class BestOpponentAttack
    {
        [JsonPropertyName("attackerTag")]
        public string AttackerTag { get; set; }

        [JsonPropertyName("defenderTag")]
        public string DefenderTag { get; set; }

        [JsonPropertyName("stars")]
        public int Stars { get; set; }

        [JsonPropertyName("destructionPercentage")]
        public int DestructionPercentage { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }

    public class Member
    {
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("townhallLevel")]
        public int TownhallLevel { get; set; }

        [JsonPropertyName("mapPosition")]
        public int MapPosition { get; set; }

        [JsonPropertyName("attacks")]
        public List<Attack> Attacks { get; } = new List<Attack>();

        [JsonPropertyName("opponentAttacks")]
        public int OpponentAttacks { get; set; }

        [JsonPropertyName("bestOpponentAttack")]
        public BestOpponentAttack BestOpponentAttack { get; set; }
    }

    public class WarClan
    {
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("badgeUrls")]
        public BadgeUrls BadgeUrls { get; set; }

        [JsonPropertyName("clanLevel")]
        public int ClanLevel { get; set; }

        [JsonPropertyName("attacks")]
        public int Attacks { get; set; }

        [JsonPropertyName("stars")]
        public int Stars { get; set; }

        [JsonPropertyName("destructionPercentage")]
        public double DestructionPercentage { get; set; }

        [JsonPropertyName("members")]
        public List<Member> Members { get; } = new List<Member>();
    }
   
    public class War
    {
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("teamSize")]
        public int TeamSize { get; set; }

        [JsonPropertyName("preparationStartTime")]
        public string PreparationStartTime { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }

        [JsonPropertyName("clan")]
        public WarClan Clan { get; set; }

        [JsonPropertyName("opponent")]
        public WarClan Opponent { get; set; }
    }


}
