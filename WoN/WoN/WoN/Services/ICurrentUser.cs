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

public interface ICalendarBuilder
{
    int[] BuildCalendar(int year, DateOnly[] publicHolidays);
}