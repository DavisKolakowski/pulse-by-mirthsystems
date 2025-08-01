using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.SpecialMenuScheduleGenerator.Enums;

public enum WeekIndex
{
    [EnumMember(Value = "first")]
    First,
    [EnumMember(Value = "second")]
    Second,
    [EnumMember(Value = "third")]
    Third,
    [EnumMember(Value = "fourth")]
    Fourth,
    [EnumMember(Value = "last")]
    Last
}