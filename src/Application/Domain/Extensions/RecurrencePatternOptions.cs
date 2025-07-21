using NodaTime;
using Application.Domain.Entities;
using Application.Domain.Constants;
using Application.Domain.Common;

namespace Application.Domain.Extensions;

public static class RecurrencePatternOptions
{
    public static RecurrencePattern Once(this SpecialMenuScheduleEntity schedule)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Once,
            Minutes = schedule.StartTime.Minute.ToString(),
            Hours = schedule.StartTime.Hour.ToString(),
            DayOfMonth = schedule.StartDate.Day.ToString(),
            Month = schedule.StartDate.Month.ToString(),
            Year = schedule.StartDate.Year.ToString()
        };
    }

    public static RecurrencePattern Daily(this SpecialMenuScheduleEntity schedule)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Daily,
            Minutes = schedule.StartTime.Minute.ToString(),
            Hours = schedule.StartTime.Hour.ToString()
        };
    }

    public static RecurrencePattern Weekdays(this SpecialMenuScheduleEntity schedule)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Weekdays,
            Minutes = schedule.StartTime.Minute.ToString(),
            Hours = schedule.StartTime.Hour.ToString(),
            DayOfWeek = "1-5"
        };
    }

    public static RecurrencePattern Weekends(this SpecialMenuScheduleEntity schedule)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Weekends,
            Minutes = schedule.StartTime.Minute.ToString(),
            Hours = schedule.StartTime.Hour.ToString(),
            DayOfWeek = "6,0"
        };
    }

    public static RecurrencePattern Weekly(this SpecialMenuScheduleEntity schedule)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Weekly,
            Minutes = schedule.StartTime.Minute.ToString(),
            Hours = schedule.StartTime.Hour.ToString(),
            DayOfWeek = ((int)schedule.StartDate.DayOfWeek).ToString()
        };
    }

    public static RecurrencePattern Monthly(this SpecialMenuScheduleEntity schedule)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Monthly,
            Minutes = schedule.StartTime.Minute.ToString(),
            Hours = schedule.StartTime.Hour.ToString(),
            DayOfMonth = schedule.StartDate.Day.ToString()
        };
    }

    public static RecurrencePattern Yearly(this SpecialMenuScheduleEntity schedule)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Yearly,
            Minutes = schedule.StartTime.Minute.ToString(),
            Hours = schedule.StartTime.Hour.ToString(),
            DayOfMonth = schedule.StartDate.Day.ToString(),
            Month = schedule.StartDate.Month.ToString()
        };
    }

    public static RecurrencePattern Custom(
        string name,
        string seconds = "*",
        string minutes = "*",
        string hours = "*",
        string dayOfMonth = "*",
        string month = "*",
        string dayOfWeek = "*",
        string year = "*")
    {
        return new RecurrencePattern
        {
            Name = name,
            Seconds = seconds,
            Minutes = minutes,
            Hours = hours,
            DayOfMonth = dayOfMonth,
            Month = month,
            DayOfWeek = dayOfWeek,
            Year = year
        };
    }
}