using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Specials;


/// <summary>
/// Group of specials by category
/// </summary>
public class SpecialCategoryGroup
{
    /// <summary>
    /// The special category identifier
    /// </summary>
    /// <example>m3n4o5p6-q7r8-4s9t-0u1v-2w3x4y5z6a7b</example>
    [Required]
    [JsonPropertyName("special_category_id")]
    [JsonPropertyOrder(1)]
    public required Guid SpecialCategoryId { get; set; }

    /// <summary>
    /// The name of this special category
    /// </summary>
    /// <example>Drink</example>
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(2)]
    public required string Name { get; set; }

    /// <summary>
    /// Optional description of this category
    /// </summary>
    /// <example>Drink specials, happy hours, and beverage promotions</example>
    [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(3)]
    public string? Description { get; set; }

    /// <summary>
    /// Icon identifier for this category
    /// </summary>
    /// <example>🍺</example>
    [MaxLength(10, ErrorMessage = "Icon cannot exceed 10 characters")]
    [JsonPropertyName("icon")]
    [JsonPropertyOrder(4)]
    public string? Icon { get; set; }

    /// <summary>
    /// The sort order for displaying this category
    /// </summary>
    /// <example>2</example>
    [JsonPropertyName("sort_order")]
    [JsonPropertyOrder(5)]
    public int SortOrder { get; set; }

    /// <summary>
    /// List of specials in this category
    /// </summary>
    [JsonPropertyName("specials")]
    [JsonPropertyOrder(6)]
    public List<SpecialItem> Specials { get; set; } = new List<SpecialItem>();
}
