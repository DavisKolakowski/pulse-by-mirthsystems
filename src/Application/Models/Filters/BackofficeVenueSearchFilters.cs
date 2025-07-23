using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Enums;

namespace Application.Models.Filters;

/// <summary>
/// Search filters for backoffice venue list
/// </summary>
public class BackofficeVenueSearchFilters : PageFilter
{
    /// <summary>
    /// Search term for venue name or description
    /// </summary>
    /// <example>Blue Oyster</example>
    [JsonPropertyName("search_term")]
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Filter by locality (city)
    /// </summary>
    /// <example>Seattle</example>
    [JsonPropertyName("locality")]
    public string? Locality { get; set; }

    /// <summary>
    /// Filter by region (state)
    /// </summary>
    /// <example>WA</example>
    [JsonPropertyName("region")]
    public string? Region { get; set; }

    /// <summary>
    /// Filter by venue categories
    /// </summary>
    /// <example>["a1b2c3d4-e5f6-4g7h-8i9j-0k1l2m3n4o5p"]</example>
    [JsonPropertyName("category_ids")]
    public List<Guid> CategoryIds { get; set; } = new List<Guid>();

    /// <summary>
    /// Filter by status
    /// </summary>
    /// <example>Active</example>
    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SearchVenuesStatusFilterSelections Status { get; set; } = SearchVenuesStatusFilterSelections.Active;

    /// <summary>
    /// Sort results by
    /// </summary>
    /// <example>Name</example>
    [JsonPropertyName("sort_by")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SearchVenuesSortByFilterSelections SortBy { get; set; } = SearchVenuesSortByFilterSelections.Name;

    /// <summary>
    /// Sort order direction
    /// </summary>
    /// <example>Ascending</example>
    [JsonPropertyName("sort_order")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SearchSortOrderFilterSelections SortOrder { get; set; } = SearchSortOrderFilterSelections.Ascending;
}