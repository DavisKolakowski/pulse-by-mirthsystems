using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;

public class WeeklyRecurrence : RecurrencePattern
{
    public override RecurrencePatternType Type => RecurrencePatternType.Weekly;
    public IsoDayOfWeek[] DaysOfWeek { get; }

    public WeeklyRecurrence(LocalDate startDate, int interval = 1, params IsoDayOfWeek[] daysOfWeek)
        : base(startDate, interval)
    {
        DaysOfWeek = daysOfWeek.Length > 0 ? daysOfWeek : [startDate.DayOfWeek];
    }

    public override IEnumerable<LocalDate> GenerateDates(LocalDate? until = null)
    {
        var currentDate = StartDate;
        var endDate = until ?? StartDate.PlusYears(1);

        while (currentDate <= endDate)
        {
            if (DaysOfWeek.Contains(currentDate.DayOfWeek))
            {
                yield return currentDate;
            }

            currentDate = currentDate.PlusDays(1);

            if (currentDate.DayOfWeek == IsoDayOfWeek.Monday && Interval > 1)
            {
                currentDate = currentDate.PlusWeeks(Interval - 1);
            }
        }
    }

    public override string GetDescription()
    {
        return DescriptionGenerator.GetWeeklyDescription(Interval, DaysOfWeek);
    }
}
