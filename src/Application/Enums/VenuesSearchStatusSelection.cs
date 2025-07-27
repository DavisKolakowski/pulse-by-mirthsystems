using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums;

public enum VenuesSearchStatusSelection
{
    [EnumMember(Value = "active")]
    Active,
    [EnumMember(Value = "all")]
    All,
    [EnumMember(Value = "inactive")]
    Inactive
}
