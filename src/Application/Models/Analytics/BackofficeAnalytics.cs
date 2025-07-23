using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Analytics;


/// <summary>
/// Overall analytics for the backoffice landing page
/// </summary>
public class BackofficeAnalytics
{
    /// <summary>
    /// Total number of venues
    /// </summary>
    /// <example>25</example>
    [JsonPropertyName("total_venues")]
    [JsonPropertyOrder(1)]
    public int TotalVenues { get; set; }

    /// <summary>
    /// Number of active venues
    /// </summary>
    /// <example>23</example>
    [JsonPropertyName("active_venues")]
    [JsonPropertyOrder(2)]
    public int ActiveVenues { get; set; }

    /// <summary>
    /// Total number of specials across all venues
    /// </summary>
    /// <example>150</example>
    [JsonPropertyName("total_specials")]
    [JsonPropertyOrder(3)]
    public int TotalSpecials { get; set; }

    /// <summary>
    /// Number of active specials across all venues
    /// </summary>
    /// <example>120</example>
    [JsonPropertyName("active_specials")]
    [JsonPropertyOrder(4)]
    public int ActiveSpecials { get; set; }

    /// <summary>
    /// Number of currently running specials based on schedules
    /// </summary>
    /// <example>45</example>
    [JsonPropertyName("currently_running_specials")]
    [JsonPropertyOrder(5)]
    public int CurrentlyRunningSpecials { get; set; }
}
