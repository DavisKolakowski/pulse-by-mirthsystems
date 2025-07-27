using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums;

public enum SpecialsSortBySelections
{
    [EnumMember(Value = "distance")]
    Distance,
    [EnumMember(Value = "item_count")]
    ItemCount
}
