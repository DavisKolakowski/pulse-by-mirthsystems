using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Entities;
using Application.Enums;
using Application.Models.Requests;

using Cronos;

using NodaTime;

namespace Application.Models.Schedules;

public static class CronPatternGenerator
{
    public static CronPattern Once(OffsetDate startDate, OffsetTime startTime)
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

    public static CronPattern Daily(OffsetTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString()
        };
    }

    public static CronPattern Weekdays(OffsetTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = "1-5"
        };
    }

    public static CronPattern Weekends(OffsetTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = "6,0"
        };
    }

    public static CronPattern Weekly(OffsetDate startDate, OffsetTime startTime)
    {
        var day = (int)startDate.DayOfWeek;
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfWeek = day.ToString(),
        };
    }

    public static CronPattern Monthly(OffsetDate startDate, OffsetTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString()
        };
    }

    public static CronPattern Yearly(OffsetDate startDate, OffsetTime startTime)
    {
        return new CronPattern
        {
            Minutes = startTime.Minute.ToString(),
            Hours = startTime.Hour.ToString(),
            DayOfMonth = startDate.Day.ToString(),
            Month = startDate.Month.ToString()
        };
    }
}
