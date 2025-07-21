using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;


/// <summary>
/// Operating schedule for a specific day
/// </summary>
public class OperatingSchedule
{
    /// <summary>
    /// Full name of the day
    /// </summary>
    /// <example>Monday</example>
    [JsonPropertyName("day_of_week_name")]
    [JsonPropertyOrder(1)]
    public required string DayOfWeekName { get; set; }

    /// <summary>
    /// Short name of the day
    /// </summary>
    /// <example>Mon</example>
    [JsonPropertyName("day_of_week_short_name")]
    [JsonPropertyOrder(2)]
    public required string DayOfWeekShortName { get; set; }

    /// <summary>
    /// ISO day number (1 = Monday, 7 = Sunday)
    /// </summary>
    /// <example>1</example>
    [JsonPropertyName("day_of_week_iso_number")]
    [JsonPropertyOrder(3)]
    public int DayOfWeekIsoNumber { get; set; }

    /// <summary>
    /// Indicates if this is a weekday
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_weekday")]
    [JsonPropertyOrder(4)]
    public bool IsWeekday { get; set; }

    /// <summary>
    /// Sort order for display
    /// </summary>
    /// <example>1</example>
    [JsonPropertyName("sort_order")]
    [JsonPropertyOrder(5)]
    public int SortOrder { get; set; }

    /// <summary>
    /// Business hours for this day
    /// </summary>
    [JsonPropertyName("business_hours")]
    [JsonPropertyOrder(6)]
    public BusinessHoursItem? BusinessHours { get; set; }
}
