using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class MonthlyOptions
{
    private readonly LocalDate _startDate;
    private readonly FrequencyOptions _parent;
    private int _interval = 1;

    public MonthlyOptions(LocalDate startDate, FrequencyOptions parent)
    {
        _startDate = startDate;
        _parent = parent;
    }

    public MonthlyOptions Interval(int interval)
    {
        if (interval < 1)
            throw new ArgumentException("Interval must be greater than 0", nameof(interval));

        _interval = interval;
        return this;
    }

    public void OnDayOfMonth(int day)
    {
        if (day < 1 || day > 31)
            throw new ArgumentException("Day must be between 1 and 31", nameof(day));

        var adjustedStartDate = new LocalDate(_startDate.Year, _startDate.Month, day);
        _parent.SetPattern(new MonthlyRecurrence(adjustedStartDate, _interval));
    }

    public void OnThe(int ordinal, IsoDayOfWeek dayOfWeek)
    {
        if (ordinal < -1 || ordinal == 0 || ordinal > 5)
            throw new ArgumentException("Ordinal must be 1-5 for first through fifth, or -1 for last", nameof(ordinal));

        _parent.SetPattern(new MonthlyNthDayRecurrence(_startDate, _interval, ordinal, dayOfWeek));
    }

    public void OnTheFirst(IsoDayOfWeek dayOfWeek) => OnThe(1, dayOfWeek);
    public void OnTheSecond(IsoDayOfWeek dayOfWeek) => OnThe(2, dayOfWeek);
    public void OnTheThird(IsoDayOfWeek dayOfWeek) => OnThe(3, dayOfWeek);
    public void OnTheFourth(IsoDayOfWeek dayOfWeek) => OnThe(4, dayOfWeek);
    public void OnTheFifth(IsoDayOfWeek dayOfWeek) => OnThe(5, dayOfWeek);
    public void OnTheLast(IsoDayOfWeek dayOfWeek) => OnThe(-1, dayOfWeek);

    public void Build()
    {
        _parent.SetPattern(new MonthlyRecurrence(_startDate, _interval));
    }

    public static implicit operator FrequencyOptions(MonthlyOptions options)
    {
        options.Build();
        return options._parent;
    }
}