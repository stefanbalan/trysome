using WoN.Components.Leaves;
using WoN.Model;

namespace WoN.Tests;

[Xunit.Trait("Area", "Leaves")]
public class Leaves_MonthTests : TestContext
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

    [Fact]
    public void Leaves_Month_HasCorrectNumberOfWeekendDays()
    {
        // arrange
        var wd = new TimeTrackingDay[5] {
            TimeTrackingDay.WorkingDay, 
            TimeTrackingDay.WorkingDay, 
            TimeTrackingDay.WeekendDay, 
            TimeTrackingDay.WeekendDay, 
            TimeTrackingDay.WorkingDay 
        };

        // act
        var cut = RenderComponent<Month>(pb => pb
            .Add(m => m.Days, wd)
        );

        // assert
        var days = cut.FindAll(".month-line > .day-we");
        Assert.Equal(2, days.Count);
    }

    [Fact]
    public void Leaves_Month_HasCorrectNumberOfVacationDays()
    {
        // arrange
        var wd = new TimeTrackingDay[5] {
            TimeTrackingDay.Vacation,
            TimeTrackingDay.WorkingDay,
            TimeTrackingDay.WeekendDay,
            TimeTrackingDay.WeekendDay,
            TimeTrackingDay.Vacation
        };

        // act
        var cut = RenderComponent<Month>(pb => pb
            .Add(m => m.Days, wd)
        );

        // assert
        var days = cut.FindAll(".month-line > .day-va");
        Assert.Equal(2, days.Count);
    }
}