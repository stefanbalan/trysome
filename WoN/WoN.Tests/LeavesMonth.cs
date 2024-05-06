using System;
using System.Collections.Generic;
using System.Linq;
using WoN.Components.Leaves;
using WoN.Model;

namespace WoN.Tests;

[Trait("Area", "Leaves")]
public class LeavesMonth : TestContext
{
    [Fact]
    public void Leaves_Month_HasCorrectNumberOfDays()
    {
        // arrange
        var wd = new TimeTrackingDay[10];

        // act
        var cut = RenderComponent<Month>(pb =>
            pb.Add(m => m.Days, wd)
        );

        // assert
        var days = cut.FindAll(".month-line > .day");
        Assert.Equal(10, days.Count);
    }

    [Theory, MemberData(nameof(PublicHolidaysTestData), TimeTrackingDay.WeekendDay)]
    public void Leaves_Month_HasCorrectNumberOfWeekendDays(TimeTrackingDay[] ttd, int expected)
    {
        // act
        var cut = RenderComponent<Month>(pb => pb.Add(m => m.Days, ttd));

        // assert
        var days = cut.FindAll(".month-line > .day-we");
        Assert.Equal(expected, days.Count);
    }

    [Theory, MemberData(nameof(PublicHolidaysTestData), TimeTrackingDay.PublicHoliday)]
    public void Leaves_Month_HasCorrectNumberOfPublicHolidayDays(TimeTrackingDay[] ttd, int expected)
    {
        // act
        var cut = RenderComponent<Month>(pb => pb.Add(m => m.Days, ttd));

        // assert
        var days = cut.FindAll(".month-line > .day-ph").ToList();
        Assert.Equal(expected, days.Count);
    }

    [Theory, MemberData(nameof(PublicHolidaysTestData), TimeTrackingDay.Vacation)]
    public void Leaves_Month_HasCorrectNumberOfVacationDays(TimeTrackingDay[] ttd, int expected)
    {
        // act
        var cut = RenderComponent<Month>(pb => pb.Add(m => m.Days, ttd));

        // assert
        var days = cut.FindAll(".month-line > .day")
            .Where(el => el.TextContent.Equals("c", StringComparison.InvariantCultureIgnoreCase))
            .ToList();
        Assert.Equal(expected, days.Count);
    }

    #region test data

    public static TimeTrackingDay[] T1 = [
        TimeTrackingDay.WeekendDay,
        TimeTrackingDay.WeekendDay,
        TimeTrackingDay.PublicHoliday,
        TimeTrackingDay.PublicHoliday,
        TimeTrackingDay.Vacation
    ];

    public static TimeTrackingDay[] T2 = [
        TimeTrackingDay.WorkingDay,
        TimeTrackingDay.WorkingDay,
        TimeTrackingDay.WorkingDay,
        TimeTrackingDay.WeekendDay,
        TimeTrackingDay.WeekendDay,
        TimeTrackingDay.Vacation,
        TimeTrackingDay.Vacation,
        TimeTrackingDay.PublicHoliday,
        TimeTrackingDay.Vacation,
        TimeTrackingDay.WorkingDay
    ];

    public static TimeTrackingDay[] T3 = {
        TimeTrackingDay.WorkingDay,
        TimeTrackingDay.WorkingDay,
        TimeTrackingDay.WorkingDay,
        TimeTrackingDay.WeekendDay,
        TimeTrackingDay.WeekendDay,
        TimeTrackingDay.Vacation,
        TimeTrackingDay.PublicHoliday,
        TimeTrackingDay.PublicHoliday,
        TimeTrackingDay.Vacation
    };

    public static TheoryData<TimeTrackingDay[], int> PublicHolidaysTestData(TimeTrackingDay dayType)
    {
        var publicHolidaysTestData = new TheoryData<TimeTrackingDay[], int> {
            { T1, T1.Count(t => t == dayType) },
            { T2, T2.Count(t => t == dayType) },
            { T3, T3.Count(t => t == dayType) }
        };
        return publicHolidaysTestData;
    }

    #endregion
}