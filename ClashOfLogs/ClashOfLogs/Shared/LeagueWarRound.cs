using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClashOfLogs.Shared;

public record LeagueWarRound(
    [property: JsonPropertyName("warTags")]
    IReadOnlyList<string> WarTags
);