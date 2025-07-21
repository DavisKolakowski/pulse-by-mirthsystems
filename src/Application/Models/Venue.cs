using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;

/// <summary>
/// Complete venue with all details
/// </summary>
public class Venue : VenueItem
{
    /// <summary>
    /// Operating schedules for the venue
    /// </summary>
    [JsonPropertyName("operating_schedules")]
    [JsonPropertyOrder(20)]
    public List<OperatingSchedule> OperatingSchedules { get; set; } = new List<OperatingSchedule>();

    /// <summary>
    /// Special menus for this venue
    /// </summary>
    [JsonPropertyName("special_menus")]
    [JsonPropertyOrder(21)]
    public List<VenueSpecialMenu> SpecialMenus { get; set; } = new List<VenueSpecialMenu>();
}