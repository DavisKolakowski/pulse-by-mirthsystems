using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using NpgsqlTypes;

namespace Application.Enums;
public enum Recurrence
{
    [PgName("once")]
    [EnumMember(Value = "once")]
    Once,
    [PgName("daily")]
    [EnumMember(Value = "daily")]
    Daily,
    [PgName("weekdays")]
    [EnumMember(Value = "weekdays")]
    Weekdays,
    [PgName("weekends")]
    [EnumMember(Value = "weekends")]
    Weekends,
    [PgName("weekly")]
    [EnumMember(Value = "weekly")]
    Weekly,
    [PgName("monthly")]
    [EnumMember(Value = "monthly")]
    Monthly,
    [PgName("yearly")]
    [EnumMember(Value = "yearly")]
    Yearly,
    [PgName("custom")]
    [EnumMember(Value = "custom")]
    Custom
}
