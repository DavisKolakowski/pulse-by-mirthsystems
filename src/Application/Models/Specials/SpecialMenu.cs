using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Models.Venues;

namespace Application.Models.Specials;


/// <summary>
/// Complete special menu with venue information for menu management page
/// </summary>
public class SpecialMenu : SpecialMenuBoardItem
{
    /// <summary>
    /// The venue this menu belongs to
    /// </summary>
    [JsonPropertyName("venue")]
    [JsonPropertyOrder(20)]
    public required VenueItem Venue { get; set; }
}
