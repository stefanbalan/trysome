namespace WoN.Data;

public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string CountryCode { get; set; }

    public Country Country { get; set; } = null!;
}

public class Calendar
{
    public int Year { get; set; } // 2024
    public required string CountryCode { get; set; }
    public required int[] NonWorkingDays { get; set; }
}

public class Country
{
    public required string Code { get; set; }
    public required string Name { get; set; }
}

public class EmployeeTimeTracking
{
    public int EmployeeId { get; set; }
    public int Year { get; set; }
    public required int[] Vacation { get; set; }
    public Employee Employee { get; set; } = null!;
}

public class PublicHoliday
{
    public int Year { get; set; }
    public required string CountryCode { get; set; }
    public DateOnly Date { get; set; }
    public required string Name { get; set; }
}