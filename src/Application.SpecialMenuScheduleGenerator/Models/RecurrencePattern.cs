using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;

public class RecurrencePattern
{
    public RecurrencePatternType Type { get; set; }
    public int Interval { get; set; }

    // For weekly patterns - using NodaTime's IsoDayOfWeek
    public List<IsoDayOfWeek>? DaysOfWeek { get; set; }
    public IsoDayOfWeek? FirstDayOfWeek { get; set; }

    // For monthly/yearly patterns
    public int? DayOfMonth { get; set; }

    // For relative patterns
    public WeekIndex? Index { get; set; }

    // For yearly patterns
    public int? Month { get; set; }

    // Additional properties for our use
    public LocalDate StartDate { get; set; }
    public LocalDate? EndDate { get; set; }
}