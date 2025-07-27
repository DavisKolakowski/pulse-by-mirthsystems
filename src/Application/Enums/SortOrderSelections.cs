using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums;

public enum SortOrderSelections
{
    [EnumMember(Value = "ascending")]
    Ascending,
    [EnumMember(Value = "descending")]
    Descending
}
