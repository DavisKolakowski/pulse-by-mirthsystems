using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Schedules;
public class CronPattern
{
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

    public override string ToString() => string.Join(" ", Seconds, Minutes, Hours, DayOfMonth, Month, DayOfWeek, Year).Trim();
}
