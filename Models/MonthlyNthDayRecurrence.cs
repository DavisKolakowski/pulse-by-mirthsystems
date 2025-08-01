using Application.SpecialMenuScheduleGenerator.Contracts;
using NodaTime;
using System.Collections.Generic;

namespace Application.SpecialMenuScheduleGenerator.Models;

public class MonthlyNthDayRecurrence : IRecurrencePattern
{
    public int Interval { get; }
    public int WeekOfMonth { get; } // 1st, 2nd, 3rd, 4th, -1 (last)
    public IsoDayOfWeek DayOfWeek { get; }

    public MonthlyNthDayRecurrence(int interval, int weekOfMonth, IsoDayOfWeek dayOfWeek)
    {
        if (interval < 1)
            throw new ArgumentException("Interval must be at least 1", nameof(interval));
        
        if (weekOfMonth is not (1 or 2 or 3 or 4 or -1))
            throw new ArgumentException("Week of month must be 1, 2, 3, 4, or -1 (last)", nameof(weekOfMonth));

        Interval = interval;
        WeekOfMonth = weekOfMonth;
        DayOfWeek = dayOfWeek;
    }

    public IEnumerable<LocalDate> GetDates(LocalDate startDate, LocalDate endDate)
    {
        var currentDate = FindFirstOccurrence(startDate);

        while (currentDate <= endDate)
        {
            yield return currentDate;
            currentDate = GetNextOccurrence(currentDate);
        }
    }

    private LocalDate FindFirstOccurrence(LocalDate startDate)
    {
        var targetDate = GetNthDayOfWeekInMonth(startDate.Year, startDate.Month, WeekOfMonth, DayOfWeek);
        
        // If the target date in the start month is before the start date, 
        // move to the next occurrence
        if (targetDate < startDate)
        {
            var nextMonth = startDate.PlusMonths(Interval);
            targetDate = GetNthDayOfWeekInMonth(nextMonth.Year, nextMonth.Month, WeekOfMonth, DayOfWeek);
        }

        return targetDate;
    }

    private LocalDate GetNextOccurrence(LocalDate currentDate)
    {
        var nextMonth = currentDate.PlusMonths(Interval);
        return GetNthDayOfWeekInMonth(nextMonth.Year, nextMonth.Month, WeekOfMonth, DayOfWeek);
    }

    private LocalDate GetNthDayOfWeekInMonth(int year, int month, int weekOfMonth, IsoDayOfWeek dayOfWeek)
    {
        var firstOfMonth = new LocalDate(year, month, 1);
        
        if (weekOfMonth == -1) // Last occurrence
        {
            var lastOfMonth = firstOfMonth.PlusMonths(1).PlusDays(-1);
            
            // Find the last occurrence of the day of week in the month
            var daysBack = ((int)lastOfMonth.DayOfWeek - (int)dayOfWeek + 7) % 7;
            return lastOfMonth.PlusDays(-daysBack);
        }
        else
        {
            // Find the first occurrence of the day of week in the month
            var daysToAdd = ((int)dayOfWeek - (int)firstOfMonth.DayOfWeek + 7) % 7;
            var firstOccurrence = firstOfMonth.PlusDays(daysToAdd);
            
            // Add weeks to get to the nth occurrence
            return firstOccurrence.PlusDays((weekOfMonth - 1) * 7);
        }
    }

    public string GetDescription()
    {
        var weekDescription = WeekOfMonth switch
        {
            1 => "1st",
            2 => "2nd", 
            3 => "3rd",
            4 => "4th",
            -1 => "last",
            _ => WeekOfMonth.ToString()
        };

        var intervalDescription = Interval == 1 
            ? "monthly" 
            : $"every {Interval} {DescriptionGenerator.GetPluralizedTimeUnit("month", Interval)}";

        return $"{intervalDescription} on the {weekDescription} {DayOfWeek}";
    }
}