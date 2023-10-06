using System.Text.Json;
using ClashOfLogs.Shared;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

public class ApiJsonLeagueWarsProvider : IJsonDataProvider
{
    private readonly ApiClient apiClient;
    private readonly string clanTag;
    private readonly ILogger<ApiJsonLeagueWarsProvider> logger;
    private JsonData? lastData;

    public ApiJsonLeagueWarsProvider(
        ILogger<ApiJsonLeagueWarsProvider> logger,
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
            var leagueGroupStr = await apiClient.GetCurrentLeagueGroupAsync(clanTag);

            if (leagueGroupStr != null)
            {
                var leagueGroup = JsonSerializer.Deserialize<LeagueWarGroup>(leagueGroupStr);

                if (leagueGroup?.Rounds != null)
                    foreach (var round in leagueGroup.Rounds)
                    {
                        // var ownWar = round.WarTags[0];
                        //
                        // var warStr = await apiClient.GetLeagueWarAsync(ownWar);

                        foreach (var warTag in round.WarTags)
                        {
                            var warStr = await apiClient.GetLeagueWarAsync(warTag);
                        }
                    }
            }


            return lastData;
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
        if (lastData?.CurrentWar?.EndTime is null)
            return fourHours;

        if (lastData.CurrentWar.EndTime > lastData.Date &&
            lastData.CurrentWar.EndTime - lastData.Date > fourHours)
            return fourHours;

        return lastData.CurrentWar.EndTime.Value - lastData.Date;
    }



}