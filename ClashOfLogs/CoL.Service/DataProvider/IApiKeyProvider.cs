using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoL.Service.DataProvider;

public interface IApiKeyProvider
{
    string? GetApiKey();
    void RenewApiKey();
}

internal class AppSettingsApiKeyProvider : IApiKeyProvider
{
    private readonly IConfiguration config;
    private readonly ILogger<AppSettingsApiKeyProvider> logger;
    private string? apikey;

    public AppSettingsApiKeyProvider(IConfiguration config,
        ILogger<AppSettingsApiKeyProvider> logger)
    {
        this.config = config;
        this.logger = logger;
        apikey = config.GetValue<string>("ApiKey");

        var change = config.GetReloadToken();
        change.RegisterChangeCallback(_ => OnConfigChange(), null);
    }

    private void OnConfigChange() => apikey = config.GetValue<string>("ApiKey");

    public string? GetApiKey() => apikey;
    public void RenewApiKey() => logger.LogInformation("RenewApiKey");
}
