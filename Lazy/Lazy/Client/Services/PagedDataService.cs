using System.Net.Http.Json;
using Lazy.Model;
using Microsoft.AspNetCore.WebUtilities;

namespace Lazy.Client.Services;

public abstract class PagedDataService<T> : DataService<T>
{
    protected readonly UserSettingsService UserSettings;

    protected PagedDataService(HttpClient httpClient, UserSettingsService userSettings) : base(httpClient)
    {
        UserSettings = userSettings;
    }

    public async Task<PagedModelResult<T>> GetPageAsync
    (
        int? pageSize,
        int? pageNumber,
        IDictionary<string, string>? queryParams = null)
    {
        var ps = pageSize ?? UserSettings.PageSize;
        var pn = pageNumber is null or < 0 ? 0 : pageNumber.Value;

        var url = QueryHelpers.AddQueryString(ApiUrl,
            new Dictionary<string, string> { { "pageSize", ps.ToString() }, { "pageNumber", pn.ToString() } });

        if (queryParams is not null) url = QueryHelpers.AddQueryString(url, queryParams);

        var pagedResult = await HttpClient.GetFromJsonAsync<PagedModelResult<T>>(url)
                          ?? new PagedModelResult<T>();

        return pagedResult;
    }
}

public class EmailTemplateDataService : PagedDataService<EmailTemplateModel>
{
    public EmailTemplateDataService(HttpClient httpClient, UserSettingsService userSettings) : base(httpClient, userSettings)
    {
    }

    protected override string ApiUrl => "/api/EmailTemplate";
}