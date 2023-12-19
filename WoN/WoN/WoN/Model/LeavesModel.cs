namespace WoN.Model;

public class LeavesModel
{
    public int StartMonth { get; set; }

    public int Months { get; set; }

    public List<bool[]> WorkingDaysMonths { get; set; } = new(3);

    public List<EmployeeLeave> EmployeeLeaves { get; set; } = new(3);
}

public record EmployeeLeave
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }

    public TimeTrackingDay[] TimeTrackingDays{ get; set; }
}

public enum TimeTrackingDay
{
    WorkingDay = 0,
    WeekendDay = 1,
    PublicHoliday = 2,
    Vacation = 3
}