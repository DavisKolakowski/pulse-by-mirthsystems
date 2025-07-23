using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using NodaTime;

namespace Application.Models.Specials;


/// <summary>
/// Special menu schedule search result item
/// </summary>
public class SpecialMenuScheduleSearchItem : SpecialMenuSchedule
{
    /// <summary>
    /// Distance from search location in miles
    /// </summary>
    /// <example>0.78</example>
    [JsonPropertyName("distance_miles")]
    [JsonPropertyOrder(21)]
    public double DistanceMiles { get; set; }

    /// <summary>
    /// Whether this special is currently running based on schedule
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_currently_running")]
    [JsonPropertyOrder(22)]
    public bool IsCurrentlyRunning { get; set; }

    /// <summary>
    /// Next occurrence of this special (if not currently running)
    /// </summary>
    /// <example>2025-07-24T16:00:00Z</example>
    [JsonPropertyName("next_occurrence")]
    [JsonPropertyOrder(23)]
    public Instant? NextOccurrence { get; set; }
}
