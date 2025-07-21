using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models;


/// <summary>
/// Venue with list-specific properties and analytics
/// </summary>
public class VenueListItem : VenueItem
{
    /// <summary>
    /// Analytics data for this venue
    /// </summary>
    [JsonPropertyName("analytics")]
    [JsonPropertyOrder(20)]
    public VenueListItemAnalytics Analytics { get; set; } = new VenueListItemAnalytics();
}