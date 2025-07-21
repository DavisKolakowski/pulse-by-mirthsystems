using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;

/// <summary>
/// Venue category with list-specific properties
/// </summary>
public class VenueCategoryListItem : VenueCategoryItem
{
    /// <summary>
    /// The sort order for displaying this category
    /// </summary>
    /// <example>1</example>
    [JsonPropertyName("sort_order")]
    [JsonPropertyOrder(10)]
    public int SortOrder { get; set; }
}