using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using NodaTime;

namespace Application.Models;


/// <summary>
/// Business hours information
/// </summary>
public class BusinessHoursItem : ItemBase
{
    /// <summary>
    /// The opening time for this day
    /// </summary>
    /// <example>09:00:00</example>
    [JsonPropertyName("open_time")]
    [JsonPropertyOrder(1)]
    public LocalTime? OpenTime { get; set; }

    /// <summary>
    /// The closing time for this day
    /// </summary>
    /// <example>22:00:00</example>
    [JsonPropertyName("close_time")]
    [JsonPropertyOrder(2)]
    public LocalTime? CloseTime { get; set; }

    /// <summary>
    /// Indicates if the venue is closed on this day
    /// </summary>
    /// <example>false</example>
    [JsonPropertyName("is_closed")]
    [JsonPropertyOrder(3)]
    public bool IsClosed { get; set; }
}
