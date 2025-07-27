using NodaTime;
using Application.Enums;
using Application.Entities;
using Application.Models.Schedules;
using Application.Constants;

namespace Application.Utilities;

public static class CronPatternGenerator
{
    public static CronPattern Once(LocalDate startDate, LocalTime startTime)
    {     
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString(),
            Month = startDate.Month.ToString(),
            Year = startDate.Year.ToString()
        };
    }

    public static CronPattern Daily(LocalTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString()
        };
    }

    public static CronPattern Weekdays(LocalTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = "1-5"
        };
    }

    public static CronPattern Weekends(LocalTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = "6,0"
        };
    }

    public static CronPattern Weekly(LocalDate startDate, LocalTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = ((int)startDate.DayOfWeek).ToString()
        };
    }

    public static CronPattern Monthly(LocalDate startDate, LocalTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString()
        };
    }

    public static CronPattern Yearly(LocalDate startDate, LocalTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString(),
            Month = startDate.Month.ToString()
        };
    }

    public static CronPattern Custom(
        string seconds = "*",
        string minutes = "*",
        string hours = "*",
        string dayOfMonth = "*",
        string month = "*",
        string dayOfWeek = "*",
        string year = "*")
    {
        return new CronPattern
        {
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