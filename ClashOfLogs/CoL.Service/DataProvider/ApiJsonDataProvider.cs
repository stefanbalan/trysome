using System.Text.Json;
using ClashOfLogs.Shared;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

public class ApiJsonDataProvider : IJsonDataProvider
{
    private readonly ApiClient apiClient;
    private readonly string clanTag;
    private readonly ILogger<ApiJsonDataProvider> logger;
    private JsonData? lastData;

    public ApiJsonDataProvider(
        ILogger<ApiJsonDataProvider> logger,
        ApiClient apiClient,
        string clanTag)
    {
        this.apiClient = apiClient;
        this.clanTag = clanTag;
        this.logger = logger;
    }

    public async Task<JsonData?> GetImportDataAsync()
    {
        try
        {
            // connect to Clash of Clans api and get CLan, Warlog and CurrentWar
            var clan = await apiClient.GetClanAsync(clanTag);
            var warlog = await apiClient.GetClanWarlogAsync(clanTag);
            var currentWar = await apiClient.GetCurrentWarAsync(clanTag);

            return lastData = new JsonData {
                Date = DateTime.Now,
                Clan = clan is null ? null : JsonSerializer.Deserialize<Clan>(clan),
                Warlog = warlog is null ? null : JsonSerializer.Deserialize<Warlog>(warlog),
                CurrentWar = currentWar is null ? null : JsonSerializer.Deserialize<WarDetail>(currentWar)
            };
        }
        catch (Exception e)
        {
            logger.LogError("Error while getting data from Clash of Clans API: {ExMessage}", e.Message);
            return null;
        }
    }

    private readonly TimeSpan fourHours = TimeSpan.FromHours(4);

    public TimeSpan GetNextImportDelay()
    {
        if (lastData?.CurrentWar?.EndTime is null ||
            lastData.CurrentWar.EndTime <= lastData.Date ||
            lastData.CurrentWar.EndTime - lastData.Date > fourHours)
            return fourHours;
        return lastData.CurrentWar.EndTime.Value - lastData.Date;
    }
}