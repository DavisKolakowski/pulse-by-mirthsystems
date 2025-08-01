using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Builders;
using Application.SpecialMenuScheduleGenerator.Contracts;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class Schedule : ISchedule
{
    public DateTimeZone TimeZone { get; }
    public LocalDate StartDate { get; }
    public LocalTime StartTime { get; }
    public LocalTime EndTime { get; }
    public LocalDate? EndDate { get; }
    public IRecurrencePattern Pattern { get; }

    public Schedule(DateTimeZone timeZone, LocalDate startDate, LocalTime startTime,
                   LocalTime endTime, LocalDate? endDate, IRecurrencePattern pattern)
    {
        TimeZone = timeZone;
        StartDate = startDate;
        StartTime = startTime;
        EndTime = endTime;
        EndDate = endDate;
        Pattern = pattern;
    }

    public static ScheduleBuilder CreateBuilder(DateTimeZone timeZone, LocalDate startDate,
                                               LocalTime startTime, LocalTime endTime)
    {
        return new ScheduleBuilder(timeZone, startDate, startTime, endTime);
    }

    public string GetFullDescription()
    {
        return DescriptionGenerator.GetFullScheduleDescription(Pattern, StartTime, EndTime, StartDate, EndDate);
    }
}