using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Contracts;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator;
public static class DescriptionGenerator
{
    public static string GetOrdinalString(int number)
    {
        if (number % 100 is >= 11 and <= 13)
            return "th";

        return (number % 10) switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th"
        };
    }

    public static string GetPluralizedTimeUnit(string unit, int count)
    {
        return count == 1 ? unit : $"{unit}s";
    }

    public static string GetDailyDescription(int interval)
    {
        return interval == 1
            ? "daily"
            : $"every {interval} {GetPluralizedTimeUnit("day", interval)}";
    }

    public static string GetWeeklyDescription(int interval, IsoDayOfWeek[] daysOfWeek)
    {
        var dayNames = string.Join(", ", daysOfWeek.Select(d => d.ToString()));

        if (interval == 1)
        {
            return $"weekly on {dayNames}";
        }

        return $"every {interval} {GetPluralizedTimeUnit("week", interval)} on {dayNames}";
    }

    public static string GetMonthlyDescription(int interval, LocalDate startDate)
    {
        var dayOfMonth = startDate.Day;
        var ordinal = GetOrdinalString(dayOfMonth);

        return interval == 1
            ? $"monthly on the {dayOfMonth}{ordinal}"
            : $"every {interval} {GetPluralizedTimeUnit("month", interval)} on the {dayOfMonth}{ordinal}";
    }

    public static string GetYearlyDescription(int interval)
    {
        return interval == 1
            ? "yearly"
            : $"every {interval} {GetPluralizedTimeUnit("year", interval)}";
    }

    public static string GetFullScheduleDescription(IRecurrencePattern pattern, LocalTime startTime,
                                                   LocalTime endTime, LocalDate startDate, LocalDate? endDate)
    {
        var timeRange = $"{startTime:HH:mm} - {endTime:HH:mm}";
        var startDateFormatted = FormatDate(startDate);
        var endDatePart = endDate.HasValue
            ? $" until {FormatDate(endDate.Value)}"
            : "";

        return $"Schedule runs {pattern.GetDescription()} at {timeRange} starting on {startDateFormatted}{endDatePart}";
    }

    private static string FormatDate(LocalDate date)
    {
        return $"{date:MMMM d}{GetOrdinalString(date.Day)}, {date.Year}";
    }
}