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
/// Request model for updating an existing special menu schedule
/// </summary>
public class UpdateSpecialMenuSchedule
{
    /// <summary>
    /// The Id of the schedule to update
    /// </summary>
    /// <example>789e0123-e89b-12d3-a456-426614174222</example>
    [Required(ErrorMessage = "Schedule Id is required")]
    [JsonPropertyName("schedule_id")]
    [JsonPropertyOrder(0)]
    public required Guid Id { get; set; }

    /// <summary>
    /// The updated recurrence pattern for this schedule
    /// </summary>
    /// <example>Weekly</example>
    [Required(ErrorMessage = "Recurrence option is required")]
    [JsonPropertyName("recurrence_option")]
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required RecurrencePatternNames RecurrenceOption { get; set; }

    /// <summary>
    /// The updated start date for this schedule
    /// </summary>
    /// <example>2025-08-01</example>
    [Required(ErrorMessage = "Start date is required")]
    [JsonPropertyName("start_date")]
    [JsonPropertyOrder(2)]
    public required LocalDate StartDate { get; set; }

    /// <summary>
    /// The updated start time of day
    /// </summary>
    /// <example>12:00:00</example>
    [Required(ErrorMessage = "Start time is required")]
    [JsonPropertyName("start_time")]
    [JsonPropertyOrder(3)]
    public required LocalTime StartTime { get; set; }

    /// <summary>
    /// The updated end time of day
    /// </summary>
    /// <example>15:00:00</example>
    [Required(ErrorMessage = "End time is required")]
    [JsonPropertyName("end_time")]
    [JsonPropertyOrder(4)]
    public required LocalTime EndTime { get; set; }

    /// <summary>
    /// Optional updated expiration date
    /// </summary>
    /// <example>2026-01-31</example>
    [JsonPropertyName("expiration_date")]
    [JsonPropertyOrder(5)]
    public LocalDate? ExpirationDate { get; set; }

    /// <summary>
    /// Whether this schedule should be active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(6)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Custom cron expression (required only when RecurrenceOption is Custom)
    /// Format: "seconds minutes hours dayOfMonth month dayOfWeek year"
    /// </summary>
    /// <example>0 0 12 ? * MON-FRI *</example>
    [JsonPropertyName("custom_cron")]
    [JsonPropertyOrder(7)]
    [RegularExpression(@"^(\S+\s+){6}\S+$", ErrorMessage = "Custom cron must have 7 space-separated values")]
    public string? CustomCron { get; set; }
}
