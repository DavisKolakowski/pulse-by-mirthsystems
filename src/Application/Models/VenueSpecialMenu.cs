using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;

/// <summary>
/// Special menu for a venue
/// </summary>
public class VenueSpecialMenu : ItemBase
{
    /// <summary>
    /// The name of this special menu
    /// </summary>
    /// <example>Weekend Brunch Special</example>
    [Required]
    [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    /// <summary>
    /// Optional description of this special menu
    /// </summary>
    /// <example>Our special weekend brunch menu featuring bottomless mimosas</example>
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    /// <summary>
    /// Schedules for this special menu
    /// </summary>
    [JsonPropertyName("schedules")]
    [JsonPropertyOrder(3)]
    public List<SpecialMenuScheduleItem> Schedules { get; set; } = new List<SpecialMenuScheduleItem>();

    /// <summary>
    /// Menu items in this special menu
    /// </summary>
    [JsonPropertyName("menu_items")]
    [JsonPropertyOrder(4)]
    public List<SpecialMenuItem> MenuItems { get; set; } = new List<SpecialMenuItem>();
}