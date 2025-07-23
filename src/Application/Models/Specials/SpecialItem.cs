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
/// Individual special item
/// </summary>
public class SpecialItem
{
    /// <summary>
    /// The special item identifier
    /// </summary>
    /// <example>n4o5p6q7-r8s9-4t0u-1v2w-3x4y5z6a7b8c</example>
    [Required]
    [JsonPropertyName("special_item_id")]
    [JsonPropertyOrder(1)]
    public required Guid SpecialItemId { get; set; }

    /// <summary>
    /// The description of this special
    /// </summary>
    /// <example>Half-Price Draft Beers - All domestic drafts at 50% off.</example>
    [Required]
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public required string Description { get; set; }

    /// <summary>
    /// Indicates whether this special is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(3)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp when this special was created
    /// </summary>
    /// <example>2025-07-23T16:00:00Z</example>
    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(998)]
    public Instant CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when this special was last updated
    /// </summary>
    /// <example>2025-07-23T16:00:00Z</example>
    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(999)]
    public Instant? UpdatedAt { get; set; }
}