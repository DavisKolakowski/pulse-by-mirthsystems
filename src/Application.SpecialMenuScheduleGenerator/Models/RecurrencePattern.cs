using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Contracts;
using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;
using NodaTime.Text;

namespace Application.SpecialMenuScheduleGenerator.Models;
public abstract class RecurrencePattern : IRecurrencePattern
{
    public abstract RecurrencePatternType Type { get; }
    public int Interval { get; protected set; } = 1;
    protected LocalDate StartDate { get; }

    protected RecurrencePattern(LocalDate startDate, int interval = 1)
    {
        StartDate = startDate;
        Interval = interval;
    }

    public abstract IEnumerable<LocalDate> GenerateDates(LocalDate? until = null);
    public abstract string GetDescription();
}
