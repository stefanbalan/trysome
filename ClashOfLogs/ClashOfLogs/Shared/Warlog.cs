using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared
{
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
        public List<WarSummary> Items { get; set; }

        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }
    }

}
