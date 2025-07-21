using System;
using System.ComponentModel.DataAnnotations;

using Application.Domain.Constants;

namespace Application.Domain.Common;

public class RecurrencePattern
{
    public string Name { get; set; } = null!;

    [RegularExpression(@"^(\*|\?|([0-5]?\d)(,[0-5]?\d)*|([0-5]?\d)-([0-5]?\d)(/[1-5]?\d)?|[0-5]?\d/[1-5]?\d)$")]
    public string Seconds { get; set; } = "*";

    [RegularExpression(@"^(\*|\?|([0-5]?\d)(,[0-5]?\d)*|([0-5]?\d)-([0-5]?\d)(/[1-5]?\d)?|[0-5]?\d/[1-5]?\d)$")]
    public string Minutes { get; set; } = "*";

    [RegularExpression(@"^(\*|\?|([01]?\d|2[0-3])(,[01]?\d|2[0-3])*|([01]?\d|2[0-3])-([01]?\d|2[0-3])(/[1-9])?|[01]?\d|2[0-3]/[1-9])$")]
    public string Hours { get; set; } = "*";

    [RegularExpression(@"^(\*|\?|([1-9]|[12]\d|3[01])(,[1-9]|[12]\d|3[01])*|([1-9]|[12]\d|3[01])-([1-9]|[12]\d|3[01])(/[1-9])?|[1-9]|[12]\d|3[01]/[1-9]|L|W|C)$")]
    public string DayOfMonth { get; set; } = "*";

    [RegularExpression(@"^(\*|\?|([1-9]|1[0-2])(,[1-9]|1[0-2])*|([1-9]|1[0-2])-([1-9]|1[0-2])(/[1-9])?|[1-9]|1[0-2]/[1-9])$")]
    public string Month { get; set; } = "*";

    [RegularExpression(@"^(\*|\?|([0-7])(,[0-7])*|([0-7])-([0-7])(/[1-7])?|[0-7]/[1-7]|L|C|#)$")]
    public string DayOfWeek { get; set; } = "*";

    [RegularExpression(@"^(\*|\?|([1-2]\d{3})(,[1-2]\d{3})*|([1-2]\d{3})-([1-2]\d{3})(/[1-9]+)?|[1-2]\d{3}/[1-9]+)$")]
    public string Year { get; set; } = "*";

    public string ToCronString() => string.Join(" ", Seconds, Minutes, Hours, DayOfMonth, Month, DayOfWeek, Year).Trim();
    public bool IsRecurring => Name != RecurrencePatternNames.Once;
}