using System;
using System.Collections.Generic;
using System.Linq;

using Application.SpecialMenuScheduleGenerator.Enums;
using Application.SpecialMenuScheduleGenerator.Models;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator;

public static class RecurrenceCalculator
{
    public static IEnumerable<LocalDate> GetOccurrences(RecurrencePattern pattern, int maxOccurrences = 100)
    {
        var currentDate = pattern.StartDate;
        var occurrences = 0;

        while (occurrences < maxOccurrences)
        {
            if (pattern.EndDate.HasValue && currentDate > pattern.EndDate.Value)
                yield break;

            switch (pattern.Type)
            {
                case RecurrencePatternType.Daily:
                    yield return currentDate;
                    currentDate = currentDate.PlusDays(pattern.Interval);
                    occurrences++;
                    break;

                case RecurrencePatternType.Weekly:
                    if (pattern.DaysOfWeek != null && pattern.DaysOfWeek.Any())
                    {
                        var firstDayOfWeek = pattern.FirstDayOfWeek ?? IsoDayOfWeek.Sunday;
                        var startOfWeek = GetStartOfWeek(currentDate, firstDayOfWeek);

                        var weekOccurrences = new List<LocalDate>();

                        for (int i = 0; i < 7; i++)
                        {
                            var checkDate = startOfWeek.PlusDays(i);
                            if (pattern.DaysOfWeek.Contains(checkDate.DayOfWeek) &&
                                checkDate >= pattern.StartDate &&
                                (!pattern.EndDate.HasValue || checkDate <= pattern.EndDate.Value))
                            {
                                weekOccurrences.Add(checkDate);
                            }
                        }

                        foreach (var occurrence in weekOccurrences.OrderBy(d => d))
                        {
                            yield return occurrence;
                            occurrences++;
                            if (occurrences >= maxOccurrences) yield break;
                        }

                        currentDate = startOfWeek.PlusWeeks(pattern.Interval);
                    }
                    break;

                case RecurrencePatternType.AbsoluteMonthly:
                    if (pattern.DayOfMonth.HasValue)
                    {
                        var daysInMonth = CalendarSystem.Iso.GetDaysInMonth(currentDate.Year, currentDate.Month);
                        var dayToUse = Math.Min(pattern.DayOfMonth.Value, daysInMonth);
                        var occurrence = new LocalDate(currentDate.Year, currentDate.Month, dayToUse);

                        if (occurrence >= pattern.StartDate && (!pattern.EndDate.HasValue || occurrence <= pattern.EndDate.Value))
                        {
                            yield return occurrence;
                            occurrences++;
                        }

                        currentDate = currentDate.PlusMonths(pattern.Interval);
                    }
                    break;

                case RecurrencePatternType.RelativeMonthly:
                    if (pattern.Index.HasValue && pattern.DaysOfWeek != null && pattern.DaysOfWeek.Any())
                    {
                        var occurrence = GetRelativeMonthlyOccurrence(currentDate, pattern.Index.Value, pattern.DaysOfWeek);
                        if (occurrence.HasValue &&
                            occurrence.Value >= pattern.StartDate &&
                            (!pattern.EndDate.HasValue || occurrence.Value <= pattern.EndDate.Value))
                        {
                            yield return occurrence.Value;
                            occurrences++;
                        }

                        currentDate = currentDate.PlusMonths(pattern.Interval);
                    }
                    break;

                case RecurrencePatternType.AbsoluteYearly:
                    if (pattern.Month.HasValue && pattern.DayOfMonth.HasValue)
                    {
                        LocalDate? occurrence = null;
                        var isValidDate = false;

                        try
                        {
                            occurrence = new LocalDate(currentDate.Year, pattern.Month.Value, pattern.DayOfMonth.Value);
                            isValidDate = true;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            // Handle leap year edge case (e.g., Feb 29)
                            isValidDate = false;
                        }

                        if (isValidDate && occurrence.HasValue &&
                            occurrence.Value >= pattern.StartDate &&
                            (!pattern.EndDate.HasValue || occurrence.Value <= pattern.EndDate.Value))
                        {
                            yield return occurrence.Value;
                            occurrences++;
                        }

                        currentDate = currentDate.PlusYears(pattern.Interval);
                    }
                    break;

                case RecurrencePatternType.RelativeYearly:
                    if (pattern.Index.HasValue && pattern.DaysOfWeek != null && pattern.DaysOfWeek.Any() && pattern.Month.HasValue)
                    {
                        var yearMonth = new LocalDate(currentDate.Year, pattern.Month.Value, 1);
                        var occurrence = GetRelativeMonthlyOccurrence(yearMonth, pattern.Index.Value, pattern.DaysOfWeek);
                        if (occurrence.HasValue &&
                            occurrence.Value >= pattern.StartDate &&
                            (!pattern.EndDate.HasValue || occurrence.Value <= pattern.EndDate.Value))
                        {
                            yield return occurrence.Value;
                            occurrences++;
                        }

                        currentDate = currentDate.PlusYears(pattern.Interval);
                    }
                    break;
            }
        }
    }

    private static LocalDate? GetRelativeMonthlyOccurrence(LocalDate month, WeekIndex index, List<IsoDayOfWeek> daysOfWeek)
    {
        var firstOfMonth = new LocalDate(month.Year, month.Month, 1);
        var lastOfMonth = firstOfMonth.PlusMonths(1).PlusDays(-1);

        var matchingDays = new List<LocalDate>();
        var current = firstOfMonth;

        while (current <= lastOfMonth)
        {
            if (daysOfWeek.Contains(current.DayOfWeek))
            {
                matchingDays.Add(current);
            }
            current = current.PlusDays(1);
        }

        if (matchingDays.Count == 0)
            return null;

        switch (index)
        {
            case WeekIndex.First:
                return matchingDays.FirstOrDefault();
            case WeekIndex.Second:
                return matchingDays.Skip(1).FirstOrDefault();
            case WeekIndex.Third:
                return matchingDays.Skip(2).FirstOrDefault();
            case WeekIndex.Fourth:
                return matchingDays.Skip(3).FirstOrDefault();
            case WeekIndex.Last:
                return matchingDays.LastOrDefault();
            default:
                return null;
        }
    }

    private static LocalDate GetStartOfWeek(LocalDate date, IsoDayOfWeek firstDayOfWeek)
    {
        var currentDayOfWeek = (int)date.DayOfWeek;
        var targetDayOfWeek = (int)firstDayOfWeek;
        var daysToSubtract = (currentDayOfWeek - targetDayOfWeek + 7) % 7;

        if (daysToSubtract == 0 && date.DayOfWeek == firstDayOfWeek)
            return date;

        return date.PlusDays(-daysToSubtract);
    }
}