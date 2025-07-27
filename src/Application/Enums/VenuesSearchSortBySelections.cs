using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Enums;

public enum VenuesSearchSortBySelections
{
    [EnumMember(Value = "name")]
    Name,
    [EnumMember(Value = "created_at")]
    CreatedAt,
    [EnumMember(Value = "locality")]
    Locality,
    [EnumMember(Value = "region")]
    Region,
    [EnumMember(Value = "status")]
    Status,
    [EnumMember(Value = "total_menus")]
    TotalMenus,
    [EnumMember(Value = "total_schedules")]
    TotalSchedules,
    [EnumMember(Value = "active_schedules")]
    ActiveSchedules,
    [EnumMember(Value = "running_schedules")]
    RunningSchedules,
}
