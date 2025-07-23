using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using NodaTime;

namespace Application.Models.BusinessHours;

/// <summary>
/// Business hours for a specific day
/// </summary>
public class BusinessHoursItem : ItemBase
{
    /// <summary>
    /// Day of week information
    /// </summary>
    [JsonPropertyName("day_of_week")]
    [JsonPropertyOrder(1)]
    public required DayOfWeekItem DayOfWeek { get; set; }

    /// <summary>
    /// The opening time for this day
    /// </summary>
    /// <example>09:00:00</example>
    [JsonPropertyName("open_time")]
    [JsonPropertyOrder(2)]
    public LocalTime? OpenTime { get; set; }

    /// <summary>
    /// The closing time for this day
    /// </summary>
    /// <example>22:00:00</example>
    [JsonPropertyName("close_time")]
    [JsonPropertyOrder(3)]
    public LocalTime? CloseTime { get; set; }

    /// <summary>
    /// Indicates if the venue is closed on this day
    /// </summary>
    /// <example>false</example>
    [JsonPropertyName("is_closed")]
    [JsonPropertyOrder(4)]
    public bool IsClosed { get; set; }

    /// <summary>
    /// Timestamp when this business hours entry was created
    /// </summary>
    /// <example>2025-07-23T09:00:00Z</example>
    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(998)]
    public Instant CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when this business hours entry was last updated
    /// </summary>
    /// <example>2025-07-23T09:00:00Z</example>
    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(999)]
    public Instant? UpdatedAt { get; set; }
}
