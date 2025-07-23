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
/// Special menu board item with schedules and categorized specials
/// </summary>
public class SpecialMenuBoardItem
{
    /// <summary>
    /// The menu identifier
    /// </summary>
    /// <example>j0k1l2m3-n4o5-4p6q-7r8s-9t0u1v2w3x4y</example>
    [Required]
    [JsonPropertyName("menu_id")]
    [JsonPropertyOrder(1)]
    public required Guid MenuId { get; set; }

    /// <summary>
    /// The name of this special menu
    /// </summary>
    /// <example>Happy Hour Specials</example>
    [Required]
    [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(2)]
    public required string Name { get; set; }

    /// <summary>
    /// Optional description of this special menu
    /// </summary>
    /// <example>Enjoy discounted drinks and appetizers during our daily happy hour.</example>
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(3)]
    public string? Description { get; set; }

    /// <summary>
    /// Schedules for this menu
    /// </summary>
    [JsonPropertyName("schedules")]
    [JsonPropertyOrder(4)]
    public List<SpecialMenuScheduleItem> Schedules { get; set; } = new List<SpecialMenuScheduleItem>();

    /// <summary>
    /// Specials grouped by category
    /// </summary>
    [JsonPropertyName("specials")]
    [JsonPropertyOrder(5)]
    public List<SpecialCategoryGroup> Specials { get; set; } = new List<SpecialCategoryGroup>();

    /// <summary>
    /// Timestamp when this menu was created
    /// </summary>
    /// <example>2025-07-23T12:00:00Z</example>
    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(998)]
    public Instant CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when this menu was last updated
    /// </summary>
    /// <example>2025-07-23T12:00:00Z</example>
    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(999)]
    public Instant? UpdatedAt { get; set; }
}