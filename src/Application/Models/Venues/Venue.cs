using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Models.BusinessHours;
using Application.Models.Categories;
using Application.Models.Location;
using Application.Models.Specials;

using NodaTime;

namespace Application.Models.Venues;


/// <summary>
/// Complete venue with specials board
/// </summary>
public class Venue : VenueItem
{
    /// <summary>
    /// Special menus board for this venue
    /// </summary>
    [JsonPropertyName("specials_board")]
    [JsonPropertyOrder(20)]
    public List<SpecialMenuBoardItem> SpecialsBoard { get; set; } = new List<SpecialMenuBoardItem>();
}