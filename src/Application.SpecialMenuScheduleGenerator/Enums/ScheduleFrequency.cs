using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.SpecialMenuScheduleGenerator.Enums;

public enum ScheduleFrequency
{
    [EnumMember(Value = "once")]
    Once,
    [EnumMember(Value = "daily")]
    Daily,
    [EnumMember(Value = "weekdays")]
    Weekdays,
    [EnumMember(Value = "weekends")]
    Weekends,
    [EnumMember(Value = "weekly")]
    Weekly,
    [EnumMember(Value = "biweekly")]
    Biweekly,
    [EnumMember(Value = "monthly")]
    Monthly,
    [EnumMember(Value = "yearly")]
    Yearly,
    [EnumMember(Value = "custom")]
    Custom
}
