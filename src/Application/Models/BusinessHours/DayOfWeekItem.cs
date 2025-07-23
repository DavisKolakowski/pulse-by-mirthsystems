using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.BusinessHours;


/// <summary>
/// Day of week information
/// </summary>
public class DayOfWeekItem : ItemBase
{
    /// <summary>
    /// The name of the day
    /// </summary>
    /// <example>Monday</example>
    [Required]
    [MaxLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    /// <summary>
    /// The short name of the day
    /// </summary>
    /// <example>MON</example>
    [Required]
    [MaxLength(3, ErrorMessage = "Short name cannot exceed 3 characters")]
    [JsonPropertyName("short_name")]
    [JsonPropertyOrder(2)]
    public required string ShortName { get; set; }

    /// <summary>
    /// ISO day number (1 = Monday, 7 = Sunday)
    /// </summary>
    /// <example>1</example>
    [JsonPropertyName("iso_number")]
    [JsonPropertyOrder(3)]
    public int IsoNumber { get; set; }

    /// <summary>
    /// Indicates if this is a weekday
    /// </summary>
    /// <example>true</example>
    [JsonPropertyName("is_weekday")]
    [JsonPropertyOrder(4)]
    public bool IsWeekday { get; set; }

    /// <summary>
    /// The sort order for displaying this day
    /// </summary>
    /// <example>2</example>
    [JsonPropertyName("sort_order")]
    [JsonPropertyOrder(5)]
    public int SortOrder { get; set; }
}
