using ClashOfLogs.Shared;
using System;

namespace CoL.Service
{
    public class JsonImport
    {
        internal DateTime Date { get; set; }
        internal Clan Clan { get; set; }
        internal Warlog WarLog { get; set; }
        internal WarDetail WarDetail { get; set; }
    }
}
