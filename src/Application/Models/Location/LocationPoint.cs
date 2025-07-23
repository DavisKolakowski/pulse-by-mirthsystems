using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Location;


/// <summary>
/// Geographic location point
/// </summary>
public class LocationPoint
{
    /// <summary>
    /// Coordinates as [longitude, latitude]
    /// </summary>
    /// <example>[-118.2437, 34.0522]</example>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(1)]
    public required double[] Coordinates { get; set; }

    /// <summary>
    /// Spatial Reference System Identifier
    /// </summary>
    /// <example>4326</example>
    [JsonPropertyName("srid")]
    [JsonPropertyOrder(2)]
    public int Srid { get; set; } = 4326;
}
