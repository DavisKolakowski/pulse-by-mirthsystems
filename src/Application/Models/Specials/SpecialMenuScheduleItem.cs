using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using NodaTime;

namespace Application.Models.Specials;


/// <summary>
/// Schedule information for a special menu
/// </summary>
public class SpecialMenuScheduleItem
{
    /// <summary>
    /// The schedule identifier
    /// </summary>
    /// <example>k1l2m3n4-o5p6-4q7r-8s9t-0u1v2w3x4y5z</example>
    [Required]
    [JsonPropertyName("schedule_id")]
    [JsonPropertyOrder(1)]
    public required Guid ScheduleId { get; set; }

    /// <summary>
    /// The name of this schedule
    /// </summary>
    /// <example>Daily</example>
    [Required]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(2)]
    public required string Name { get; set; }

    /// <summary>
    /// The date when this schedule starts
    /// </summary>
    /// <example>2025-07-23</example>
    [Required]
    [JsonPropertyName("start_date")]
    [JsonPropertyOrder(3)]
    public required LocalDate StartDate { get; set; }

    /// <summary>
    /// The time of day when the special menu becomes available
    /// </summary>
    /// <example>16:00:00</example>
    [Required]
    [JsonPropertyName("start_time")]
    [JsonPropertyOrder(4)]
    public required LocalTime StartTime { get; set; }

    /// <summary>
    /// The time of day when the special menu ends
    /// </summary>
    /// <example>19:00:00</example>
    [Required]
    [JsonPropertyName("end_time")]
    [JsonPropertyOrder(5)]
    public required LocalTime EndTime { get; set; }

    /// <summary>
    /// Optional date when this schedule expires and will no longer recur
    /// </summary>
    /// <example>2025-08-31</example>
    [JsonPropertyName("expiration_date")]
    [JsonPropertyOrder(6)]
    public LocalDate? ExpirationDate { get; set; }

    /// <summary>
    /// The cron string representing the recurrence pattern
    /// </summary>
    /// <example>* * * * * * *</example>
    [Required]
    [JsonPropertyName("cron_string")]
    [JsonPropertyOrder(7)]
    public required string CronString { get; set; }

    /// <summary>
    /// Indicates whether this schedule is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(8)]
    public bool IsActive { get; set; } = true;
}
