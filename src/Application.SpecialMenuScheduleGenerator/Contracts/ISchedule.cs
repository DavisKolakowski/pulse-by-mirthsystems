using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Contracts;

public interface ISchedule
{
    DateTimeZone TimeZone { get; }
    LocalDate StartDate { get; }
    LocalTime StartTime { get; }
    LocalTime EndTime { get; }
    LocalDate? EndDate { get; }
    IRecurrencePattern Pattern { get; }
}