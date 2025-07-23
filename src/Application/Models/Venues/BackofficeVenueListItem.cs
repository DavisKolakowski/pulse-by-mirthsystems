using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Models.Analytics;
using Application.Models.Categories;

using NodaTime;

namespace Application.Models.Venues;

/// <summary>
/// Venue list item for backoffice
/// </summary>
public class BackofficeVenueListItem : ItemBase
{
    /// <summary>
    /// The name of the venue
    /// </summary>
    /// <example>The Blue Oyster Bar</example>
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    /// <summary>
    /// Optional description of the venue
    /// </summary>
    /// <example>A cozy neighborhood bar with live music every weekend</example>
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    /// <summary>
    /// The primary category for this venue
    /// </summary>
    [JsonPropertyName("primary_category")]
    [JsonPropertyOrder(3)]
    public required VenueCategoryItem PrimaryCategory { get; set; }

    /// <summary>
    /// The secondary category for this venue
    /// </summary>
    [JsonPropertyName("secondary_category")]
    [JsonPropertyOrder(4)]
    public VenueCategoryItem? SecondaryCategory { get; set; }

    /// <summary>
    /// City or locality
    /// </summary>
    /// <example>Seattle</example>
    [Required]
    [MaxLength(100, ErrorMessage = "Locality cannot exceed 100 characters")]
    [JsonPropertyName("locality")]
    [JsonPropertyOrder(5)]
    public required string Locality { get; set; }

    /// <summary>
    /// State or region
    /// </summary>
    /// <example>WA</example>
    [Required]
    [MaxLength(50, ErrorMessage = "Region cannot exceed 50 characters")]
    [JsonPropertyName("region")]
    [JsonPropertyOrder(6)]
    public required string Region { get; set; }

    /// <summary>
    /// Indicates whether this venue is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(7)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Analytics data for this venue
    /// </summary>
    [JsonPropertyName("analytics")]
    [JsonPropertyOrder(8)]
    public BackofficeVenueListItemAnalytics Analytics { get; set; } = new BackofficeVenueListItemAnalytics();

    /// <summary>
    /// Timestamp when this venue was created
    /// </summary>
    /// <example>2025-07-23T08:00:00Z</example>
    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(998)]
    public Instant CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when this venue was last updated
    /// </summary>
    /// <example>2025-07-23T08:00:00Z</example>
    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(999)]
    public Instant? UpdatedAt { get; set; }
}
