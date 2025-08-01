using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using NodaTime;

namespace Application.SpecialMenuScheduleGenerator.Enums;

[Flags]
public enum Days
{
    [EnumMember(Value = "sunday")]
    Sunday = 1 << 0,
    [EnumMember(Value = "monday")]
    Monday = 1 << 1,
    [EnumMember(Value = "tuesday")]
    Tuesday = 1 << 2,
    [EnumMember(Value = "wednesday")]
    Wednesday = 1 << 3,
    [EnumMember(Value = "thursday")]
    Thursday = 1 << 4,
    [EnumMember(Value = "friday")]
    Friday = 1 << 5,
    [EnumMember(Value = "saturday")]
    Saturday = 1 << 6,
    [EnumMember(Value = "any")]
    Any = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday,
    [EnumMember(Value = "weekday")]
    Weekday = Monday | Tuesday | Wednesday | Thursday | Friday,
    [EnumMember(Value = "weekend_day")]
    WeekendDay = Saturday | Sunday
}