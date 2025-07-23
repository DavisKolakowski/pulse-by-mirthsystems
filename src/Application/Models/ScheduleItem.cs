using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using NodaTime;

namespace Application.Models;


/// <summary>
/// Schedule information for a special menu
/// </summary>
public class ScheduleItem : ItemBase
{
    /// <summary>
    /// The date when this schedule starts
    /// </summary>
    /// <example>2025-07-21</example>
    [Required]
    [JsonPropertyName("start_date")]
    [JsonPropertyOrder(1)]
    public required LocalDate StartDate { get; set; }

    /// <summary>
    /// The time of day when the special menu becomes available
    /// </summary>
    /// <example>11:30:00</example>
    [Required]
    [JsonPropertyName("start_time")]
    [JsonPropertyOrder(2)]
    public required LocalTime StartTime { get; set; }

    /// <summary>
    /// The time of day when the special menu ends
    /// </summary>
    /// <example>14:30:00</example>
    [Required]
    [JsonPropertyName("end_time")]
    [JsonPropertyOrder(3)]
    public required LocalTime EndTime { get; set; }

    /// <summary>
    /// Optional date when this schedule expires and will no longer recur
    /// </summary>
    /// <example>2025-12-31</example>
    [JsonPropertyName("expiration_date")]
    [JsonPropertyOrder(4)]
    public LocalDate? ExpirationDate { get; set; }

    /// <summary>
    /// Indicates whether this schedule is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(5)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The recurrence pattern for this schedule
    /// </summary>
    [Required]
    [JsonPropertyName("recurrence_pattern")]
    [JsonPropertyOrder(6)]
    public required RecurrencePattern RecurrencePattern { get; set; }
}
