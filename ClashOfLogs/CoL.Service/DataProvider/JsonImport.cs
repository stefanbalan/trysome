using ClashOfLogs.Shared;

namespace CoL.Service.DataProvider;

public record JsonImport
{
    internal DateTime Date { get; set; }
    internal Clan? Clan { get; set; }
    internal Warlog? WarLog { get; set; }
    internal WarDetail? WarDetail { get; set; }
}