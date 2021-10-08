﻿using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    public class Label
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iconUrls")]
        public IconUrls IconUrls { get; set; }
    }
}