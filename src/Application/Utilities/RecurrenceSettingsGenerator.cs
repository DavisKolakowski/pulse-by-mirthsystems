using NodaTime;
using NodaTime.Extensions;
using System.Globalization;
using Application.Enums;
using Application.Models.Schedules;
using Application.Constants;

namespace Application.Utilities;

public static class RecurrenceSettingsGenerator
{
    private static string GetOrdinalSuffix(int day)
    {
        if (day % 100 is 11 or 12 or 13)
        {
            return "th";
        }
        return (day % 10) switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th"
        };
    }

    public static RecurrenceSettings Once(OffsetDate startDate, OffsetTime startTime)
    {
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Once.ToString().ToLowerInvariant(),
            Description = $"Starts once on {startDate.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)} at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)}.",
            CronPattern = new CronPattern
            {
                Minutes = startTime.Minute.ToString(),
                Hours = startTime.Hour.ToString(),
                DayOfMonth = startDate.Day.ToString(),
                Month = startDate.Month.ToString(),
                Year = startDate.Year.ToString()
            }
        };
    }

    public static RecurrenceSettings Daily(OffsetTime startTime)
    {
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Daily.ToString().ToLowerInvariant(),
            Description = $"Starts daily at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)}.",
            CronPattern = new CronPattern
            {
                Minutes = startTime.Minute.ToString(),
                Hours = startTime.Hour.ToString()
            }
        };
    }

    public static RecurrenceSettings Weekdays(OffsetTime startTime)
    {
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Weekdays.ToString().ToLowerInvariant(),
            Description = $"Starts every weekday (Monday to Friday) at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)}.",
            CronPattern = new CronPattern
            {
                Minutes = startTime.Minute.ToString(),
                Hours = startTime.Hour.ToString(),
                DayOfWeek = "1-5"
            }
        };
    }

    public static RecurrenceSettings Weekends(OffsetTime startTime)
    {
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Weekends.ToString().ToLowerInvariant(),
            Description = $"Starts every weekend (Saturday and Sunday) at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)}.",
            CronPattern = new CronPattern
            {
                Minutes = startTime.Minute.ToString(),
                Hours = startTime.Hour.ToString(),
                DayOfWeek = "6,0"
            }
        };
    }

    public static RecurrenceSettings Weekly(OffsetDate startDate, OffsetTime startTime)
    {
        var day = (int)startDate.DayOfWeek;
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Weekly.ToString().ToLowerInvariant(),
            Description = $"Starts weekly on {startDate.DayOfWeek.ToString()} at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)}.",
            CronPattern = new CronPattern
            {
                Minutes = startTime.Minute.ToString(),
                Hours = startTime.Hour.ToString(),
                DayOfWeek = day.ToString(),
            }
        };
    }

    public static RecurrenceSettings Monthly(OffsetDate startDate, OffsetTime startTime)
    {
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Monthly.ToString().ToLowerInvariant(),
            Description = $"Starts monthly on the {startDate.Day}{GetOrdinalSuffix(startDate.Day)} at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)}.",
            CronPattern = new CronPattern
            {
                Minutes = startTime.Minute.ToString(),
                Hours = startTime.Hour.ToString(),
                DayOfMonth = startDate.Day.ToString()
            }
        };
    }

    public static RecurrenceSettings Yearly(OffsetDate startDate, OffsetTime startTime)
    {
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Yearly.ToString().ToLowerInvariant(),
            Description = $"Starts yearly on {startDate.Date.ToString("MMMM dd", CultureInfo.InvariantCulture)} at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)}.",
            CronPattern = new CronPattern
            {
                Minutes = startTime.Minute.ToString(),
                Hours = startTime.Hour.ToString(),
                DayOfMonth = startDate.Day.ToString(),
                Month = startDate.Month.ToString()
            }
        };
    }

    public static RecurrenceSettings Custom(
        OffsetDate startDate,
        OffsetTime startTime,
        CronPattern cronPattern,
        string? description)
    {
        return new RecurrenceSettings
        {
            Name = RecurrenceOption.Custom.ToString().ToLowerInvariant(),
            Description = description ?? $"Starts on {startDate.Date.ToString("MMMM dd", CultureInfo.InvariantCulture)} at {startTime.TimeOfDay.ToString("hh:mm tt", CultureInfo.InvariantCulture)} based on a custom cron pattern defined by the user.",
            CronPattern = new CronPattern
            {
                Seconds = cronPattern.Seconds,
                Minutes = cronPattern.Minutes,
                Hours = cronPattern.Hours,
                DayOfMonth = cronPattern.DayOfMonth,
                Month = cronPattern.Month,
                DayOfWeek = cronPattern.DayOfWeek,
                Year = cronPattern.Year
            }
        };
    }
}