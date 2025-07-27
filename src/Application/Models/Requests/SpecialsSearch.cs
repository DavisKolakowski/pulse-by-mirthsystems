using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Enums;

using NetTopologySuite.Geometries;

using NodaTime;

namespace Application.Models.Requests;


/// <summary>
/// Search filters for public specials search
/// </summary>
public class SpecialsSearch : PageInfo
{
    /// <summary>
    /// Longitude coordinate for search center
    /// </summary>
    /// <example>-122.3321</example>
    [Required]
    [JsonPropertyName("longitude")]
    [JsonPropertyOrder(1)]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
    public double Longitude { get; set; }

    /// <summary>
    /// Latitude coordinate for search center
    /// </summary>
    /// <example>47.6062</example>
    [Required]
    [JsonPropertyName("latitude")]
    [JsonPropertyOrder(2)]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
    public double Latitude { get; set; }

    /// <summary>
    /// Search radius in miles
    /// </summary>
    /// <example>5.0</example>
    [Required]
    [JsonPropertyName("radius_miles")]
    [JsonPropertyOrder(3)]
    [Range(0.1, 50, ErrorMessage = "Radius must be between 0.1 and 50 miles")]
    public double RadiusMiles { get; set; }

    /// <summary>
    /// Optional search term for venue name or special description
    /// </summary>
    /// <example>happy hour</example>
    [JsonPropertyName("search_term")]
    [JsonPropertyOrder(4)]
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Search for specials on a specific date
    /// </summary>
    /// <example>2025-07-23</example>
    [JsonPropertyName("search_date")]
    [JsonPropertyOrder(5)]
    public LocalDate? SearchDate { get; set; }

    /// <summary>
    /// Search for specials at a specific time
    /// </summary>
    /// <example>18:30:00</example>
    [JsonPropertyName("search_time")]
    [JsonPropertyOrder(6)]
    public LocalTime? SearchTime { get; set; }

    /// <summary>
    /// Filter by special categories
    /// </summary>
    /// <example>["m3n4o5p6-q7r8-4s9t-0u1v-2w3x4y5z6a7b"]</example>
    [JsonPropertyName("special_category_ids")]
    [JsonPropertyOrder(7)]
    public List<Guid> SpecialCategoryIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Filter by venue categories
    /// </summary>
    /// <example>["a1b2c3d4-e5f6-4g7h-8i9j-0k1l2m3n4o5p"]</example>
    [JsonPropertyName("venue_category_ids")]
    [JsonPropertyOrder(8)]
    public List<Guid> VenueCategoryIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Only show currently running specials based on schedule
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("currently_running")]
    [JsonPropertyOrder(9)]
    public bool CurrentlyRunning { get; set; } = true;

    /// <summary>
    /// Sort results by
    /// </summary>
    /// <example>Distance</example>
    [JsonPropertyName("sort_by")]
    [JsonPropertyOrder(10)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SpecialsSortBySelection SortBy { get; set; } = SpecialsSortBySelection.Distance;

    /// <summary>
    /// Sort order direction
    /// </summary>
    /// <example>Ascending</example>
    [JsonPropertyName("sort_order")]
    [JsonPropertyOrder(11)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SortOrderSelection SortOrder { get; set; } = SortOrderSelection.Ascending;
}