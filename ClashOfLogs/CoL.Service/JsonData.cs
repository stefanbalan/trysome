using ClashOfLogs.Shared;

using System;

namespace CoL.Service
{
    public class JsonData
    {
        public DateTime Date { get; internal set; }

        public Clan Clan { get; internal set; }
        public Warlog Warlog { get; internal set; }
        public WarDetail CurrentWar { get; internal set; }
    }
}
