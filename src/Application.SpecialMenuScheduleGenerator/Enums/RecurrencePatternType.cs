using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.SpecialMenuScheduleGenerator.Enums;

public enum RecurrencePatternType
{
    [EnumMember(Value = "daily")]
    Daily,
    [EnumMember(Value = "weekly")]
    Weekly,
    [EnumMember(Value = "absolute_monthly")]
    AbsoluteMonthly,
    [EnumMember(Value = "relative_monthly")]
    RelativeMonthly,
    [EnumMember(Value = "absolute_yearly")]
    AbsoluteYearly,
    [EnumMember(Value = "relative_yearly")]
    RelativeYearly
}
