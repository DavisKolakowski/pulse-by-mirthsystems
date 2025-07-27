using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Enums;

namespace Application.Models.Requests;

/// <summary>
/// Search filters for backoffice venue list
/// </summary>
public class VenuesSearch : PageInfo
{
    /// <summary>
    /// Search term for venue name, description, location, or other text fields
    /// </summary>
    /// <example>Blue Oyster</example>
    [JsonPropertyName("search_term")]
    public string? SearchTerm { get; set; }

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
    public VenuesSearchStatusSelections Status { get; set; } = VenuesSearchStatusSelections.Active;

    /// <summary>
    /// Sort results by
    /// </summary>
    /// <example>Name</example>
    [JsonPropertyName("sort_by")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VenuesSearchSortBySelections SortBy { get; set; } = VenuesSearchSortBySelections.Name;

    /// <summary>
    /// Sort order direction
    /// </summary>
    /// <example>Ascending</example>
    [JsonPropertyName("sort_order")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SortOrderSelections SortOrder { get; set; } = SortOrderSelections.Ascending;
}