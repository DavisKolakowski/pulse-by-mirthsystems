using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Application.SpecialMenuScheduleGenerator.Enums;
using Application.SpecialMenuScheduleGenerator.Models;

using NodaTime;
using NodaTime.Text;

namespace Application.SpecialMenuScheduleGenerator;

public class FrequencyBuilder
{
    private LocalDate _startDate;
    private LocalTime _startTime;
    private LocalTime _endTime;
    private LocalDate? _expirationDate;
    private ScheduleFrequency _scheduleFrequency;
    private int _interval = 1;
    private List<IsoDayOfWeek> _daysOfWeek = new List<IsoDayOfWeek>();
    private WeekIndex? _weekIndex;
    private Days? _days;

    public FrequencyBuilder WithStartDate(LocalDate date)
    {
        _startDate = date;
        return this;
    }

    public FrequencyBuilder WithStartTime(LocalTime time)
    {
        _startTime = time;
        return this;
    }

    public FrequencyBuilder WithEndTime(LocalTime time)
    {
        _endTime = time;
        return this;
    }

    public FrequencyBuilder WithExpirationDate(LocalDate date)
    {
        _expirationDate = date;
        return this;
    }

    public FrequencyBuilder WithScheduleFrequency(ScheduleFrequency scheduleFrequency)
    {
        _scheduleFrequency = scheduleFrequency;
        return this;
    }

    public FrequencyBuilder WithInterval(int interval)
    {
        _interval = interval;
        return this;
    }

    public FrequencyBuilder WithDaysOfWeek(List<IsoDayOfWeek> days)
    {
        _daysOfWeek = days;
        return this;
    }

    public FrequencyBuilder WithWeekIndex(WeekIndex index)
    {
        _weekIndex = index;
        return this;
    }

    public FrequencyBuilder WithDays(Days days)
    {
        _days = days;
        return this;
    }

    public Frequency Build()
    {
        switch (_scheduleFrequency)
        {
            case ScheduleFrequency.Once:
                return BuildOnce();
            case ScheduleFrequency.Daily:
                return BuildDaily(1);
            case ScheduleFrequency.Weekdays:
                return BuildWeekdays();
            case ScheduleFrequency.Weekends:
                return BuildWeekends();
            case ScheduleFrequency.Weekly:
                return BuildWeekly(1, new List<IsoDayOfWeek> { _startDate.DayOfWeek });
            case ScheduleFrequency.Biweekly:
                return BuildWeekly(2, new List<IsoDayOfWeek> { _startDate.DayOfWeek });
            case ScheduleFrequency.Monthly:
                return BuildMonthlyDayOf(1);
            case ScheduleFrequency.Yearly:
                return BuildYearly(1);
            default:
                throw new InvalidOperationException($"Unsupported schedule frequency: {_scheduleFrequency}");
        }
    }

    public Frequency BuildCustomDaily()
    {
        return BuildDaily(_interval);
    }

    public Frequency BuildCustomWeekly()
    {
        return BuildWeekly(_interval, _daysOfWeek);
    }

    public Frequency BuildCustomMonthly()
    {
        return BuildMonthlyOnThe(_interval, _weekIndex!.Value, _days!.Value);
    }

    private Frequency BuildOnce()
    {
        var description = $"Occurs once on {FormatDate(_startDate)} at {FormatTimeRange()}";

        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.Daily,
            Interval = 1,
            StartDate = _startDate,
            EndDate = _startDate // Same as start for once
        };

        return new Frequency
        {
            Option = ScheduleFrequency.Once,
            Description = description,
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private Frequency BuildDaily(int interval)
    {
        var description = new StringBuilder();
        description.Append($"Occurs every {(interval == 1 ? "day" : $"{interval} days")} starting on {FormatDate(_startDate)} at {FormatTimeRange()}");

        if (_expirationDate.HasValue)
        {
            description.Append($" and expiring on {FormatDate(_expirationDate.Value)}");
        }

        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.Daily,
            Interval = interval,
            StartDate = _startDate,
            EndDate = _expirationDate
        };

        return new Frequency
        {
            Option = _scheduleFrequency,
            Description = description.ToString(),
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private Frequency BuildWeekdays()
    {
        var description = new StringBuilder();
        description.Append($"Occurs every weekday starting on {FormatDate(_startDate)} at {FormatTimeRange()}");

        if (_expirationDate.HasValue)
        {
            description.Append($" and expiring on {FormatDate(_expirationDate.Value)}");
        }

        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.Weekly,
            Interval = 1,
            DaysOfWeek = ConvertDaysToIsoDaysOfWeek(Days.Weekday),
            FirstDayOfWeek = IsoDayOfWeek.Sunday,
            StartDate = _startDate,
            EndDate = _expirationDate
        };

        return new Frequency
        {
            Option = ScheduleFrequency.Weekdays,
            Description = description.ToString(),
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private Frequency BuildWeekends()
    {
        var description = new StringBuilder();
        description.Append($"Occurs every weekend starting on {FormatDate(_startDate)} at {FormatTimeRange()}");

        if (_expirationDate.HasValue)
        {
            description.Append($" and expiring on {FormatDate(_expirationDate.Value)}");
        }

        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.Weekly,
            Interval = 1,
            DaysOfWeek = ConvertDaysToIsoDaysOfWeek(Days.WeekendDay),
            FirstDayOfWeek = IsoDayOfWeek.Sunday,
            StartDate = _startDate,
            EndDate = _expirationDate
        };

        return new Frequency
        {
            Option = ScheduleFrequency.Weekends,
            Description = description.ToString(),
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private Frequency BuildWeekly(int interval, List<IsoDayOfWeek> daysOfWeek)
    {
        var description = new StringBuilder();
        var daysDescription = GetDaysDescription(daysOfWeek);

        description.Append($"Occurs every {(interval == 1 ? "week" : $"{interval} weeks")} on {daysDescription} starting on {FormatDate(_startDate)} at {FormatTimeRange()}");

        if (_expirationDate.HasValue)
        {
            description.Append($" and expiring on {FormatDate(_expirationDate.Value)}");
        }

        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.Weekly,
            Interval = interval,
            DaysOfWeek = daysOfWeek,
            FirstDayOfWeek = IsoDayOfWeek.Sunday,
            StartDate = _startDate,
            EndDate = _expirationDate
        };

        return new Frequency
        {
            Option = _scheduleFrequency,
            Description = description.ToString(),
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private Frequency BuildMonthlyDayOf(int interval)
    {
        var description = new StringBuilder();
        description.Append($"Occurs {(interval == 1 ? "monthly" : $"every {interval} months")} on day {_startDate.Day} starting on {FormatDate(_startDate)} at {FormatTimeRange()}");

        if (_expirationDate.HasValue)
        {
            description.Append($" and expiring on {FormatDate(_expirationDate.Value)}");
        }

        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.AbsoluteMonthly,
            Interval = interval,
            DayOfMonth = _startDate.Day,
            StartDate = _startDate,
            EndDate = _expirationDate
        };

        return new Frequency
        {
            Option = ScheduleFrequency.Monthly,
            Description = description.ToString(),
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private Frequency BuildMonthlyOnThe(int interval, WeekIndex index, Days dayType)
    {
        var description = new StringBuilder();
        var indexText = GetIndexText(index);
        var dayTypeText = GetDayTypeDescription(dayType);

        description.Append($"Occurs {(interval == 1 ? "monthly" : $"every {interval} months")} on the {indexText} {dayTypeText} starting on {FormatDate(_startDate)} at {FormatTimeRange()}");

        if (_expirationDate.HasValue)
        {
            description.Append($" and expiring on {FormatDate(_expirationDate.Value)}");
        }

        var daysOfWeek = ConvertDaysToIsoDaysOfWeek(dayType);
        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.RelativeMonthly,
            Interval = interval,
            DaysOfWeek = daysOfWeek,
            Index = index,
            StartDate = _startDate,
            EndDate = _expirationDate
        };

        return new Frequency
        {
            Option = ScheduleFrequency.Monthly,
            Description = description.ToString(),
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private Frequency BuildYearly(int interval)
    {
        var description = new StringBuilder();
        var monthDay = $"{_startDate.Month}/{_startDate.Day}";

        description.Append($"Occurs {(interval == 1 ? "yearly" : $"every {interval} years")} on {monthDay} starting on {FormatDate(_startDate)} at {FormatTimeRange()}");

        if (_expirationDate.HasValue)
        {
            description.Append($" and expiring on {FormatDate(_expirationDate.Value)}");
        }

        var pattern = new RecurrencePattern
        {
            Type = RecurrencePatternType.AbsoluteYearly,
            Interval = interval,
            DayOfMonth = _startDate.Day,
            Month = _startDate.Month,
            StartDate = _startDate,
            EndDate = _expirationDate
        };

        return new Frequency
        {
            Option = ScheduleFrequency.Yearly,
            Description = description.ToString(),
            Pattern = pattern,
            StartTime = _startTime,
            EndTime = _endTime
        };
    }

    private List<IsoDayOfWeek> ConvertDaysToIsoDaysOfWeek(Days days)
    {
        var result = new List<IsoDayOfWeek>();

        // Check each individual day flag
        if (days.HasFlag(Days.Sunday)) result.Add(IsoDayOfWeek.Sunday);
        if (days.HasFlag(Days.Monday)) result.Add(IsoDayOfWeek.Monday);
        if (days.HasFlag(Days.Tuesday)) result.Add(IsoDayOfWeek.Tuesday);
        if (days.HasFlag(Days.Wednesday)) result.Add(IsoDayOfWeek.Wednesday);
        if (days.HasFlag(Days.Thursday)) result.Add(IsoDayOfWeek.Thursday);
        if (days.HasFlag(Days.Friday)) result.Add(IsoDayOfWeek.Friday);
        if (days.HasFlag(Days.Saturday)) result.Add(IsoDayOfWeek.Saturday);

        return result;
    }

    private string FormatTimeRange()
    {
        var timePattern = LocalTimePattern.Create("HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        return $"{timePattern.Format(_startTime)}-{timePattern.Format(_endTime)}";
    }

    private string FormatDate(LocalDate date)
    {
        var datePattern = LocalDatePattern.Create("MMMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);
        return datePattern.Format(date);
    }

    private string GetDaysDescription(List<IsoDayOfWeek> days)
    {
        if (days.Count == 5 && days.All(d => d >= IsoDayOfWeek.Monday && d <= IsoDayOfWeek.Friday))
        {
            return "weekdays";
        }

        if (days.Count == 2 && days.Contains(IsoDayOfWeek.Saturday) && days.Contains(IsoDayOfWeek.Sunday))
        {
            return "weekends";
        }

        if (days.Count == 7)
        {
            return "all days";
        }

        if (days.Count == 1)
        {
            return days[0].ToString() + "s"; // e.g., "Fridays"
        }

        return string.Join(", ", days.Select(d => d.ToString()));
    }

    private string GetDayTypeDescription(Days dayType)
    {
        switch (dayType)
        {
            case Days.Any:
                return "day";
            case Days.Weekday:
                return "weekday";
            case Days.WeekendDay:
                return "weekend day";
            case Days.Sunday:
                return "Sunday";
            case Days.Monday:
                return "Monday";
            case Days.Tuesday:
                return "Tuesday";
            case Days.Wednesday:
                return "Wednesday";
            case Days.Thursday:
                return "Thursday";
            case Days.Friday:
                return "Friday";
            case Days.Saturday:
                return "Saturday";
            default:
                return dayType.ToString().ToLower();
        }
    }

    private string GetIndexText(WeekIndex index)
    {
        switch (index)
        {
            case WeekIndex.First:
                return "first";
            case WeekIndex.Second:
                return "second";
            case WeekIndex.Third:
                return "third";
            case WeekIndex.Fourth:
                return "fourth";
            case WeekIndex.Last:
                return "last";
            default:
                return index.ToString().ToLower();
        }
    }
}