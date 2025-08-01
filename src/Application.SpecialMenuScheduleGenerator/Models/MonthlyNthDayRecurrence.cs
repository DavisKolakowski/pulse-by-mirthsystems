using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.SpecialMenuScheduleGenerator.Enums;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Models;
public class MonthlyNthDayRecurrence : RecurrencePattern
{
    public override RecurrencePatternType Type => RecurrencePatternType.Monthly;
    public int Ordinal { get; }
    public IsoDayOfWeek DayOfWeek { get; }

    public MonthlyNthDayRecurrence(LocalDate startDate, int interval, int ordinal, IsoDayOfWeek dayOfWeek)
        : base(startDate, interval)
    {
        Ordinal = ordinal;
        DayOfWeek = dayOfWeek;
    }

    public override IEnumerable<LocalDate> GenerateDates(LocalDate? until = null)
    {
        var currentDate = StartDate;
        var endDate = until ?? StartDate.PlusYears(1);

        while (currentDate <= endDate)
        {
            var nthDay = GetNthDayOfMonth(currentDate.Year, currentDate.Month, Ordinal, DayOfWeek);
            if (nthDay.HasValue && nthDay.Value >= currentDate && nthDay.Value <= endDate)
            {
                yield return nthDay.Value;
            }

            currentDate = currentDate.PlusMonths(Interval);
        }
    }

    public override string GetDescription()
    {
        var ordinalText = Ordinal switch
        {
            1 => "first",
            2 => "second",
            3 => "third",
            4 => "fourth",
            5 => "fifth",
            -1 => "last",
            _ => $"{Ordinal}th"
        };

        var timeUnit = DescriptionGenerator.GetPluralizedTimeUnit("month", Interval);
        var intervalText = Interval == 1 ? "monthly" : $"every {Interval} {timeUnit}";

        return $"{intervalText} on the {ordinalText} {DayOfWeek}";
    }

    private static LocalDate? GetNthDayOfMonth(int year, int month, int ordinal, IsoDayOfWeek dayOfWeek)
    {
        var firstDay = new LocalDate(year, month, 1);
        var lastDay = firstDay.PlusMonths(1).PlusDays(-1);

        if (ordinal == -1)
        {
            for (var date = lastDay; date >= firstDay; date = date.PlusDays(-1))
            {
                if (date.DayOfWeek == dayOfWeek)
                    return date;
            }
        }
        else
        {
            var count = 0;
            for (var date = firstDay; date <= lastDay; date = date.PlusDays(1))
            {
                if (date.DayOfWeek == dayOfWeek)
                {
                    count++;
                    if (count == ordinal)
                        return date;
                }
            }
        }
        return null;
    }
}