using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;


/// <summary>
/// Analytics data for venue list items
/// </summary>
public class VenueListItemAnalytics
{
    /// <summary>
    /// Count of active menus with schedules
    /// </summary>
    /// <example>5</example>
    [JsonPropertyName("active_menus_scheduled")]
    [JsonPropertyOrder(1)]
    public int ActiveMenusScheduled { get; set; }

    /// <summary>
    /// Count of currently running scheduled menus
    /// </summary>
    /// <example>2</example>
    [JsonPropertyName("running_scheduled_menus")]
    [JsonPropertyOrder(2)]
    public int RunningScheduledMenus { get; set; }

    /// <summary>
    /// Total count of menus
    /// </summary>
    /// <example>8</example>
    [JsonPropertyName("total_menus")]
    [JsonPropertyOrder(3)]
    public int TotalMenus { get; set; }
}
