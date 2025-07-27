using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Enums;

using NodaTime;

namespace Application.Models.Requests;

/// <summary>
/// Request model for creating a new special menu schedule
/// </summary>
public class CreateSpecialMenuSchedule
{
    /// <summary>
    /// The Id of the special menu to schedule
    /// </summary>
    /// <example>456e7890-e89b-12d3-a456-426614174111</example>
    [Required(ErrorMessage = "Special menu Id is required")]
    [JsonPropertyName("special_menu_id")]
    [JsonPropertyOrder(1)]
    public required Guid SpecialMenuId { get; set; }

    /// <summary>
    /// The recurrence pattern for this schedule
    /// </summary>
    /// <example>Daily</example>
    [Required(ErrorMessage = "Recurrence option is required")]
    [JsonPropertyName("recurrence_option")]
    [JsonPropertyOrder(2)]
    public required string RecurrenceOption { get; set; }

    /// <summary>
    /// The date when this schedule should start
    /// </summary>
    /// <example>2025-07-21</example>
    [Required(ErrorMessage = "Start date is required")]
    [JsonPropertyName("start_date")]
    [JsonPropertyOrder(3)]
    public required LocalDate StartDate { get; set; }

    /// <summary>
    /// The time of day when the special menu becomes available
    /// </summary>
    /// <example>11:30:00</example>
    [Required(ErrorMessage = "Start time is required")]
    [JsonPropertyName("start_time")]
    [JsonPropertyOrder(4)]
    public required LocalTime StartTime { get; set; }

    /// <summary>
    /// The time of day when the special menu ends
    /// </summary>
    /// <example>14:30:00</example>
    [Required(ErrorMessage = "End time is required")]
    [JsonPropertyName("end_time")]
    [JsonPropertyOrder(5)]
    public required LocalTime EndTime { get; set; }

    /// <summary>
    /// Optional date when this schedule expires and will no longer recur
    /// </summary>
    /// <example>2025-12-31</example>
    [JsonPropertyName("expiration_date")]
    [JsonPropertyOrder(6)]
    public LocalDate? ExpirationDate { get; set; }

    /// <summary>
    /// Whether this schedule should be active immediately upon creation
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(7)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Custom cron expression (required only when RecurrenceOption is Custom)
    /// Format: "seconds minutes hours dayOfMonth month dayOfWeek year"
    /// </summary>
    /// <example>0 0 12 ? * MON-FRI *</example>
    [JsonPropertyName("custom_cron")]
    [JsonPropertyOrder(8)]
    [RegularExpression(@"^(\S+\s+){6}\S+$", ErrorMessage = "Custom cron must have 7 space-separated values")]
    public string? CustomCron { get; set; }
}