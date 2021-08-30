using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

    public class Cursors
    {
    }

    public class Paging
    {
        [JsonPropertyName("cursors")]
        public Cursors Cursors { get; set; }
    }

    public class Warlog
    {
        [JsonPropertyName("items")]
        public List<WarSummary> Items { get; } = new List<WarSummary>();

        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }
    }

}
