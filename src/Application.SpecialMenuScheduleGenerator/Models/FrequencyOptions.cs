using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Contracts;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class FrequencyOptions
{
    private readonly LocalDate _startDate;
    private IRecurrencePattern? _pattern;

    public FrequencyOptions(LocalDate startDate)
    {
        _startDate = startDate;
    }

    public DailyOptions UseDaily() => new DailyOptions(_startDate, this);
    public WeeklyOptions UseWeekly() => new WeeklyOptions(_startDate, this);
    public MonthlyOptions UseMonthly() => new MonthlyOptions(_startDate, this);
    public YearlyOptions UseYearly() => new YearlyOptions(_startDate, this);

    internal void SetPattern(IRecurrencePattern pattern)
    {
        _pattern = pattern;
    }

    internal IRecurrencePattern BuildPattern()
    {
        return _pattern ?? throw new InvalidOperationException("No frequency pattern configured");
    }
}
