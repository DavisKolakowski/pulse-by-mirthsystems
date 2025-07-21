using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;

/// <summary>
/// Base class for all model items with a unique identifier
/// </summary>
public abstract class ItemBase
{
    /// <summary>
    /// Unique identifier for the item
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("id")]
    [JsonPropertyOrder(-100)]
    public Guid Id { get; set; }
}
