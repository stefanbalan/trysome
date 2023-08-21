using CoL.Service.DataProvider;
using Microsoft.Extensions.Logging.Abstractions;

namespace CoL.Service.Tests;

public class ApiClientTests
{
    [Fact]
    public async Task Test()
    {
        var httpClient = new HttpClient() {
            BaseAddress = new Uri("https://api.clashofclans.com/v1/"),
        };
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        var apiclient = new ApiClient(
            NullLogger<ApiClient>.Instance,
            httpClient,
            new MockApiKeyProvider());

        var clanJson1 = await apiclient.GetClanAsync("#2Y2L9G8J");
        var clanJson2 = await apiclient.GetClanAsync("#2L82JLL9R");
    }


    class MockApiKeyProvider : IApiKeyProvider
    {
        public string GetApiKey() =>
            "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJ" +
            "jNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsI" +
            "mp0aSI6IjU2NmUwNmI3LWUxNjgtNDkxOC1hZTdkLTE1MTgwZTA2YWEzNyIsImlhdCI6MTY4OTYyMjUx" +
            "Niwic3ViIjoiZGV2ZWxvcGVyLzVmM2U4MDZmLWVhNmYtMmJhZS00YzBlLTNjMWRiOGE3NTRkMiIsInN" +
            "jb3BlcyI6WyJjbGFzaCJdLCJsaW1pdHMiOlt7InRpZXIiOiJkZXZlbG9wZXIvc2lsdmVyIiwidHlwZS" +
            "I6InRocm90dGxpbmcifSx7ImNpZHJzIjpbIjE4OC4yNi45My4yNyJdLCJ0eXBlIjoiY2xpZW50In1df" +
            "Q.bWhFWGwyw8FtEwF1VdIMN4CNgL7Mjvp1tzLWOvKv3ys84MQWAardbsTfYf_t7bFRy_NpvTISP7N9H" +
            "0syxIoKqw";

        public void RenewApiKey() { }
    }
}