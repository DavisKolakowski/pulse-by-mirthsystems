using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Categories;


/// <summary>
/// Basic venue category information
/// </summary>
public class VenueCategoryItem : ItemBase
{
    /// <summary>
    /// The name of this venue category
    /// </summary>
    /// <example>Restaurant</example>
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    /// <summary>
    /// Optional description of this category
    /// </summary>
    /// <example>Full-service dining establishments</example>
    [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    /// <summary>
    /// Icon identifier for this category
    /// </summary>
    /// <example>🍽️</example>
    [MaxLength(10, ErrorMessage = "Icon cannot exceed 10 characters")]
    [JsonPropertyName("icon")]
    [JsonPropertyOrder(3)]
    public string? Icon { get; set; }
}
