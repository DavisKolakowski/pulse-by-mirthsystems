using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Contracts;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class RecurrenceOptions
{
    private readonly LocalDate _startDate;
    private IRecurrencePattern? _pattern;

    public RecurrenceOptions(LocalDate startDate)
    {
        _startDate = startDate;
    }

    // Preset methods - these are the convenient shortcuts
    public void UseDaily() => _pattern = new DailyRecurrence(_startDate, 1);

    public void UseWeekdays() => _pattern = new WeeklyRecurrence(_startDate, 1,
        IsoDayOfWeek.Monday, IsoDayOfWeek.Tuesday, IsoDayOfWeek.Wednesday,
        IsoDayOfWeek.Thursday, IsoDayOfWeek.Friday);

    public void UseWeekends() => _pattern = new WeeklyRecurrence(_startDate, 1,
        IsoDayOfWeek.Saturday, IsoDayOfWeek.Sunday);

    public void UseWeekly() => _pattern = new WeeklyRecurrence(_startDate, 1, _startDate.DayOfWeek);

    public void UseBiweekly() => _pattern = new WeeklyRecurrence(_startDate, 2, _startDate.DayOfWeek);

    public void UseMonthly() => _pattern = new MonthlyRecurrence(_startDate, 1);

    public void UseYearly() => _pattern = new YearlyRecurrence(_startDate, 1);

    // Custom configuration method
    public void UseCustom(Action<FrequencyOptions> configure)
    {
        var frequencyOptions = new FrequencyOptions(_startDate);
        configure(frequencyOptions);
        _pattern = frequencyOptions.BuildPattern();
    }

    internal IRecurrencePattern BuildPattern()
    {
        return _pattern ?? throw new InvalidOperationException("No recurrence pattern configured");
    }
}