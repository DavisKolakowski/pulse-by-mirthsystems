using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class WeeklyOptions
{
    private readonly LocalDate _startDate;
    private readonly FrequencyOptions _parent;
    private int _interval = 1;
    private IsoDayOfWeek[] _daysOfWeek;

    public WeeklyOptions(LocalDate startDate, FrequencyOptions parent)
    {
        _startDate = startDate;
        _parent = parent;
        _daysOfWeek = [startDate.DayOfWeek]; // Default to start date's day
    }

    public WeeklyOptions Interval(int interval)
    {
        if (interval < 1)
            throw new ArgumentException("Interval must be greater than 0", nameof(interval));

        _interval = interval;
        return this;
    }

    public WeeklyOptions OnDays(params IsoDayOfWeek[] days)
    {
        if (days.Length == 0)
            throw new ArgumentException("At least one day must be specified", nameof(days));

        _daysOfWeek = days;
        return this;
    }

    public WeeklyOptions OnWeekdays()
    {
        _daysOfWeek = [IsoDayOfWeek.Monday, IsoDayOfWeek.Tuesday, IsoDayOfWeek.Wednesday,
                       IsoDayOfWeek.Thursday, IsoDayOfWeek.Friday];
        return this;
    }

    public WeeklyOptions OnWeekends()
    {
        _daysOfWeek = [IsoDayOfWeek.Saturday, IsoDayOfWeek.Sunday];
        return this;
    }

    public void Build()
    {
        _parent.SetPattern(new WeeklyRecurrence(_startDate, _interval, _daysOfWeek));
    }

    public static implicit operator FrequencyOptions(WeeklyOptions options)
    {
        options.Build();
        return options._parent;
    }
}
