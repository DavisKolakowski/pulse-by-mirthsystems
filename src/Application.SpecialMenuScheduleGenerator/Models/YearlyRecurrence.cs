using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class YearlyRecurrence : RecurrencePattern
{
    public override RecurrencePatternType Type => RecurrencePatternType.Yearly;

    public YearlyRecurrence(LocalDate startDate, int interval = 1)
        : base(startDate, interval)
    {
    }

    public override IEnumerable<LocalDate> GenerateDates(LocalDate? until = null)
    {
        var currentDate = StartDate;
        var endDate = until ?? StartDate.PlusYears(10);

        while (currentDate <= endDate)
        {
            yield return currentDate;
            currentDate = currentDate.PlusYears(Interval);
        }
    }

    public override string GetDescription()
    {
        return DescriptionGenerator.GetYearlyDescription(Interval);
    }
}