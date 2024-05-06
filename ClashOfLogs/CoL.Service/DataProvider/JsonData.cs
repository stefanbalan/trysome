using ClashOfLogs.Shared;

namespace CoL.Service.DataProvider;

public class JsonData
{
    public DateTime Date { get; internal set; }

    public Clan? Clan { get; internal set; }
    public Warlog? Warlog { get; internal set; }
    public WarDetail? CurrentWar { get; internal set; }
}