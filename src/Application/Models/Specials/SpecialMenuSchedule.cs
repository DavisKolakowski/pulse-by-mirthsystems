using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Specials;


/// <summary>
/// Complete schedule with menu information for schedule management page
/// </summary>
public class SpecialMenuSchedule : SpecialMenuScheduleItem
{
    /// <summary>
    /// The menu this schedule belongs to
    /// </summary>
    [JsonPropertyName("menu")]
    [JsonPropertyOrder(20)]
    public required SpecialMenuBoardItem Menu { get; set; }
}
