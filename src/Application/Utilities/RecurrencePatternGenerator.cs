using NodaTime;
using Application.Models;
using Application.Enums;
using Application.Entities;

namespace Application.Utilities;

public static class RecurrencePatternGenerator
{
    public static RecurrencePattern Once(LocalDate startDate, LocalTime startTime)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Once.ToString(),
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString(),
            Month = startDate.Month.ToString(),
            Year = startDate.Year.ToString()
        };
    }

    public static RecurrencePattern Daily(LocalTime startTime)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Daily.ToString(),
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString()
        };
    }

    public static RecurrencePattern Weekdays(LocalTime startTime)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Weekdays.ToString(),
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = "1-5"
        };
    }

    public static RecurrencePattern Weekends(LocalTime startTime)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Weekends.ToString(),
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = "6,0"
        };
    }

    public static RecurrencePattern Weekly(LocalDate startDate, LocalTime startTime)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Weekly.ToString(),
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = ((int)startDate.DayOfWeek).ToString()
        };
    }

    public static RecurrencePattern Monthly(LocalDate startDate, LocalTime startTime)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Monthly.ToString(),
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString()
        };
    }

    public static RecurrencePattern Yearly(LocalDate startDate, LocalTime startTime)
    {
        return new RecurrencePattern
        {
            Name = RecurrencePatternNames.Yearly.ToString(),
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString(),
            Month = startDate.Month.ToString()
        };
    }

    public static RecurrencePattern Custom(
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
            Name = RecurrencePatternNames.Custom.ToString(),
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