using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;

/// <summary>
/// Basic venue role information
/// </summary>
public class VenueRoleItem : ItemBase
{
    /// <summary>
    /// The name of the role
    /// </summary>
    /// <example>venue-manager</example>
    [Required]
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public required string Name { get; set; }

    /// <summary>
    /// Description of the role
    /// </summary>
    /// <example>Can manage venue details and specials</example>
    [Required]
    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public required string Description { get; set; }
}
