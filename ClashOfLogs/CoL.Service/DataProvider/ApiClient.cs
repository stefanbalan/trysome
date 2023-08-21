using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

public class ApiClient
{
    private readonly ILogger<ApiClient> logger;
    private readonly HttpClient client;
    private readonly IApiKeyProvider apiKeyProvider;
    private readonly JsonBackup jsonBackup;

    public ApiClient(
        ILogger<ApiClient> logger,
        HttpClient client,
        IApiKeyProvider apiKeyProvider,
        JsonBackup jsonBackup
    )
    {
        this.logger = logger;
        this.client = client;
        this.apiKeyProvider = apiKeyProvider;
        this.jsonBackup = jsonBackup;
    }


    private async Task<string?> ApiRequestAsync(string url)
    {
        logger.LogInformation("API request: {Url}", url);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKeyProvider.GetApiKey());
        try
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();

            logger.LogError("Failed API request: {ReasonCode} {ReasonPhrase}",
                response.StatusCode, response.ReasonPhrase);
            if (response.ReasonPhrase == "accessDenied.invalidIp")
            {
                // renew api key
                apiKeyProvider.RenewApiKey();
            }

            return null;
        }
        catch (Exception e)
        {
            logger.LogError("Error while getting data from Clash of Clans API: {ExMessage}", e.Message);
            return null;
        }
    }

    public async Task<string?> GetClanAsync(string clanTag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(clanTag)}");
        if (json != null) await jsonBackup.BackupJsonAsync("clan", json);
        return json;
    }

    public async Task<string?> GetClanMembersAsync(string clanTag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(clanTag)}/members");
        if (json != null) await jsonBackup.BackupJsonAsync("members", json);
        return json;
    }

    public async Task<string?> GetClanWarlogAsync(string tag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(tag)}/warlog");
        if (json != null) await jsonBackup.BackupJsonAsync("warlog", json);
        return json;
    }

    public async Task<string?> GetCurrentWarAsync(string tag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(tag)}/currentwar");
        if (json != null) await jsonBackup.BackupJsonAsync("currentwar", json);
        return json;
    }

    public async Task<string?> GetCurrentLeagueGroupAsync(string tag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(tag)}/currentwar/leaguegroup");
        if (json != null) await jsonBackup.BackupJsonAsync("currentleaguegroup", json);
        return json;
    }
}