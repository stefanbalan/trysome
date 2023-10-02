using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
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
        var apiKey = apiKeyProvider.GetApiKey();
        logger.LogInformation("API key: {ApiKeyStart}...{ApiKeyEnd}", apiKey.Substring(0, 15), apiKey.Substring(apiKey.Length - 15));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);
        try
        {
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return content;
            }

            logger.LogError("Failed API request: {ReasonCode} {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);

            if (response.ReasonPhrase.StartsWith("accessDenied"))
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

    record ApiError(string Reason, string Message);

    public async Task<string?> GetClanAsync(string clanTag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(clanTag)}");
        if (json != null) await jsonBackup.BackupJsonAsync(json, "clan", DateTime.Now);
        return json;
    }

    public async Task<string?> GetClanMembersAsync(string clanTag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(clanTag)}/members");
        if (json != null) await jsonBackup.BackupJsonAsync(json, "members", DateTime.Now);
        return json;
    }

    public async Task<string?> GetClanWarlogAsync(string tag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(tag)}/warlog");
        if (json != null) await jsonBackup.BackupJsonAsync(json, "warlog", DateTime.Now);
        return json;
    }

    public async Task<string?> GetCurrentWarAsync(string tag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(tag)}/currentwar");
        if (json != null) await jsonBackup.BackupJsonAsync(json, "currentwar", DateTime.Now);
        return json;
    }

    public async Task<string?> GetCurrentLeagueGroupAsync(string tag)
    {
        var json = await ApiRequestAsync($"clans/{Uri.EscapeDataString(tag)}/currentwar/leaguegroup");
        if (json != null) await jsonBackup.BackupJsonAsync(json, "leaguegroup", DateTime.Now);
        return json;
    }

    public async Task<string?> GetLeagueWarAsync(string tag)
    {
        var json = await ApiRequestAsync($"clanwarleagues/wars/{Uri.EscapeDataString(tag)}");
        var wartag = tag.Replace("#", "");
        if (json != null) await jsonBackup.BackupJsonAsync(json, $"leaguewar_{wartag}", DateTime.Now);
        return json;
    }
}