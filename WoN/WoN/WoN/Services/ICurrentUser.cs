using WoN.Data;

namespace WoN.Services;

public interface ICurrentUser
{
    string GetCountryCode();
}

public class CurrentUserMock : ICurrentUser
{
    public string GetCountryCode() => "RO";
}
