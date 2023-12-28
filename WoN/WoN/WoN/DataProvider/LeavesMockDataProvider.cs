using WoN.Model;

namespace WoN.DataProvider;

public class LeavesMockDataProvider : IDataProvider<LeavesModel>
{
    public Task<LeavesModel> GetDataAsync(dynamic? criteria = null)
    {
        var result = new LeavesModel {
            StartMonth = 1,
            Months = 1,
            EmployeeLeaves = [
                new() {
                    EmployeeId = 1,
                    EmployeeName = "John Doe",
                    TimeTrackingDays = new Dictionary<int, TimeTrackingDay[]> {
                        {
                            1, new[] {
                                TimeTrackingDay.PublicHoliday, TimeTrackingDay.PublicHoliday,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.PublicHoliday,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.PublicHoliday
                            }
                        }
                    }
                },

                new() {
                    EmployeeId = 2,
                    EmployeeName = "Jane Doe",
                    TimeTrackingDays = new Dictionary<int, TimeTrackingDay[]> {
                        {
                            1, new[] {
                                TimeTrackingDay.PublicHoliday, TimeTrackingDay.PublicHoliday,
                                TimeTrackingDay.Vacation, TimeTrackingDay.Vacation, TimeTrackingDay.Vacation,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.PublicHoliday,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay,
                                TimeTrackingDay.WeekendDay, TimeTrackingDay.WeekendDay,
                                TimeTrackingDay.WorkingDay, TimeTrackingDay.WorkingDay, TimeTrackingDay.PublicHoliday
                            }
                        }
                    }
                }
            ]
        };

        return Task.FromResult(result);
    }
}