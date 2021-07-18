using Newtonsoft.Json;

using System.Collections.Generic;

namespace ClashOfLogs.Shared
{
    public class Cursors
    {
    }

    public class Paging
    {
        [JsonProperty("cursors")]
        public Cursors Cursors { get; set; }
    }

    /// <summary>
    /// Root for https://api.clashofclans.com/v1/clans/%232L82JLL9R/members
    /// </summary>
    public class MemberList
    {
        [JsonProperty("items")]
        public List<Member> Items { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}
