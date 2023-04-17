using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record Cursors
{
}

public record Paging
{
    [JsonPropertyName("cursors")]
    public Cursors Cursors { get; set; }
}

public record Warlog
{
    [JsonPropertyName("items")]
    public List<WarSummary> Items { get; set; }

    [JsonPropertyName("paging")]
    public Paging Paging { get; set; }
}