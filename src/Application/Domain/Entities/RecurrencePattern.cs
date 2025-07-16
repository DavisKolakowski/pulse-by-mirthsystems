using System;
using System.ComponentModel.DataAnnotations;

using Application.Domain.Constants;

namespace Application.Domain.Entities;

public class RecurrencePattern
{
    private RecurrencePattern() { }

    public RecurrencePattern(
        string name,
        string seconds = "*",
        string minutes = "*",
        string hours = "*",
        string dayOfMonth = "*",
        string month = "*",
        string dayOfWeek = "*",
        string year = "*")
    {
        Name = name;
        Seconds = seconds;
        Minutes = minutes;
        Hours = hours;
        DayOfMonth = dayOfMonth;
        Month = month;
        DayOfWeek = dayOfWeek;
        Year = year;
    }

    public string Name { get; private set; } = null!;

    [RegularExpression(@"^(\*|\?|([0-5]?\d)(,[0-5]?\d)*|([0-5]?\d)-([0-5]?\d)(/[1-5]?\d)?|[0-5]?\d/[1-5]?\d)$")]
    public string Seconds { get; private set; } = null!;

    [RegularExpression(@"^(\*|\?|([0-5]?\d)(,[0-5]?\d)*|([0-5]?\d)-([0-5]?\d)(/[1-5]?\d)?|[0-5]?\d/[1-5]?\d)$")]
    public string Minutes { get; private set; } = null!;

    [RegularExpression(@"^(\*|\?|([01]?\d|2[0-3])(,[01]?\d|2[0-3])*|([01]?\d|2[0-3])-([01]?\d|2[0-3])(/[1-9])?|[01]?\d|2[0-3]/[1-9])$")]
    public string Hours { get; private set; } = null!;

    [RegularExpression(@"^(\*|\?|([1-9]|[12]\d|3[01])(,[1-9]|[12]\d|3[01])*|([1-9]|[12]\d|3[01])-([1-9]|[12]\d|3[01])(/[1-9])?|[1-9]|[12]\d|3[01]/[1-9]|L|W|C)$")]
    public string DayOfMonth { get; private set; } = null!;

    [RegularExpression(@"^(\*|\?|([1-9]|1[0-2])(,[1-9]|1[0-2])*|([1-9]|1[0-2])-([1-9]|1[0-2])(/[1-9])?|[1-9]|1[0-2]/[1-9])$")]
    public string Month { get; private set; } = null!;

    [RegularExpression(@"^(\*|\?|([0-7])(,[0-7])*|([0-7])-([0-7])(/[1-7])?|[0-7]/[1-7]|L|C|#)$")]
    public string DayOfWeek { get; private set; } = null!;

    [RegularExpression(@"^(\*|\?|([1-2]\d{3})(,[1-2]\d{3})*|([1-2]\d{3})-([1-2]\d{3})(/[1-9]+)?|[1-2]\d{3}/[1-9]+)$")]
    public string Year { get; private set; } = null!;

    public string ToCronString() => string.Join(" ", Seconds, Minutes, Hours, DayOfMonth, Month, DayOfWeek, Year).Trim();
    public bool IsRecurring => Name != RecurrencePatternNames.Once;
}