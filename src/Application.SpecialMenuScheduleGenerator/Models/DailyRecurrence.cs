using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;

public class DailyRecurrence : RecurrencePattern
{
    public override RecurrencePatternType Type => RecurrencePatternType.Daily;

    public DailyRecurrence(LocalDate startDate, int interval = 1)
        : base(startDate, interval)
    {
    }

    public override IEnumerable<LocalDate> GenerateDates(LocalDate? until = null)
    {
        var currentDate = StartDate;
        var endDate = until ?? StartDate.PlusYears(1);

        while (currentDate <= endDate)
        {
            yield return currentDate;
            currentDate = currentDate.PlusDays(Interval);
        }
    }

    public override string GetDescription()
    {
        return DescriptionGenerator.GetDailyDescription(Interval);
    }
}
