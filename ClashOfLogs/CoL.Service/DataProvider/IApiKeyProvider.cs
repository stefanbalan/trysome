using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace CoL.Service.DataProvider;

public interface IApiKeyProvider
{
    string? GetApiKey();
}

internal class AppSettingsApiKeyProvider : IApiKeyProvider
{
    private readonly IConfiguration config;
    private IChangeToken change;
    private string? apikey;

    public AppSettingsApiKeyProvider(IConfiguration config)
    {
        this.config = config;
        apikey = config.GetValue<string>("ApiKey");

        change = config.GetReloadToken();
        change.RegisterChangeCallback(_ => OnConfigChange(), null);
    }

    private void OnConfigChange() => apikey = config.GetValue<string>("ApiKey");

    public string? GetApiKey() => apikey;
}
