using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Analytics;


/// <summary>
/// Analytics data for backoffice venue list items
/// </summary>
public class BackofficeVenueListItemAnalytics
{
    /// <summary>
    /// Total count of special menus
    /// </summary>
    /// <example>8</example>
    [JsonPropertyName("total_special_menus")]
    [JsonPropertyOrder(1)]
    public int TotalSpecialMenus { get; set; }

    /// <summary>
    /// Count of active special menus
    /// </summary>
    /// <example>5</example>
    [JsonPropertyName("active_special_menus")]
    [JsonPropertyOrder(2)]
    public int ActiveSpecialMenus { get; set; }

    /// <summary>
    /// Total count of specials across all menus
    /// </summary>
    /// <example>25</example>
    [JsonPropertyName("total_specials")]
    [JsonPropertyOrder(3)]
    public int TotalSpecials { get; set; }

    /// <summary>
    /// Count of active specials across all menus
    /// </summary>
    /// <example>20</example>
    [JsonPropertyName("active_specials")]
    [JsonPropertyOrder(4)]
    public int ActiveSpecials { get; set; }
}
