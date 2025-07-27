using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using NpgsqlTypes;

namespace Application.Enums;

public enum SpecialMenuScheduleStatus
{
    [PgName("running")]
    [EnumMember(Value = "running")]
    Running,
    [PgName("scheduled")]
    [EnumMember(Value = "scheduled")]
    Scheduled,
    [PgName("expired")]
    [EnumMember(Value = "expired")]
    Expired,
    [PgName("disabled")]
    [EnumMember(Value = "disabled")]
    Disabled,
}
