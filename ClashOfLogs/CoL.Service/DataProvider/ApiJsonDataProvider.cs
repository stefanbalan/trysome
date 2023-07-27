using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using ClashOfLogs.Shared;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;


public class ApiJsonDataProvider : IJsonDataProvider
{
    private readonly IApiKeyProvider apiKeyProvider;
    private readonly HttpClient client;
    private readonly string clanTag;
    private readonly ILogger<ApiJsonDataProvider> logger;

    public ApiJsonDataProvider(ILogger<ApiJsonDataProvider> logger,
        IApiKeyProvider apiKeyProvider, HttpClient client,
        string clanTag)
    {
        this.apiKeyProvider = apiKeyProvider;
        this.client = client;
        this.clanTag = clanTag;
        this.logger = logger;
        this.apiKeyProvider = apiKeyProvider;
    }

    public bool HasImportData() => !string.IsNullOrWhiteSpace(apiKeyProvider.GetApiKey()); // maybe add a ping to the api

    public async Task<JsonData?> GetImportDataAsync()
    {
        try
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKeyProvider.GetApiKey());
            // connect to Clash of Clans api and get CLan, Warlog and CurrentWar
            var clan = await GetClanByTagAsync(clanTag);
            var warlog = await GetClanWarlogByTagAsync(clanTag);
            var currentWar = await GetCurrentWarByTagAsync(clanTag);

            return new JsonData {
                Date = DateTime.Now,
                Clan = JsonSerializer.Deserialize<Clan>(clan),
                Warlog = JsonSerializer.Deserialize<Warlog>(warlog),
                CurrentWar = JsonSerializer.Deserialize<WarDetail>(currentWar)
            };
        }
        catch (Exception e)
        {
            logger.LogError("Error while getting data from Clash of Clans API: {ExMessage}", e.Message);
            return null;
        }
    }


    private async Task<string> GetClanByTagAsync(string tag)
    {
        var response = await client.GetAsync($"clans/{Uri.EscapeDataString(tag)}");
        return await response.Content.ReadAsStringAsync();
    }

    private async Task<string> GetClanWarlogByTagAsync(string tag)
    {
        var response = await client.GetAsync($"clans/{Uri.EscapeDataString(tag)}/warlog");
        return await response.Content.ReadAsStringAsync();
    }

    private async Task<string> GetCurrentWarByTagAsync(string tag)
    {
        var response = await client.GetAsync($"clans/{Uri.EscapeDataString(tag)}/currentwar");
        return await response.Content.ReadAsStringAsync();
    }
}