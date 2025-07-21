using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;


/// <summary>
/// Individual special menu item
/// </summary>
public class SpecialMenuItem : ItemBase
{
    /// <summary>
    /// The description of this special
    /// </summary>
    /// <example>Half-Price Wings</example>
    [Required]
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(1)]
    public required string Description { get; set; }

    /// <summary>
    /// Indicates whether this special is currently active
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(2)]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The category of this special
    /// </summary>
    [JsonPropertyName("category")]
    [JsonPropertyOrder(3)]
    public SpecialCategoryItem? Category { get; set; }
}
