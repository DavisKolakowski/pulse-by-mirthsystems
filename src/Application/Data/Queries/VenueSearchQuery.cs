using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Enums;

namespace Application.Queries;

public class VenueSearchQuery
{
    public string? SearchTerm { get; set; }

    public string? Locality { get; set; }
    public string? Region { get; set; }

    public List<Guid> CategoryIds { get; set; } = new List<Guid>();

    public SearchVenuesStatusFilterSelections Status { get; set; } = SearchVenuesStatusFilterSelections.Active;

    public SearchVenuesSortByFilterSelections SortBy { get; set; } = SearchVenuesSortByFilterSelections.Name;
    public SearchSortOrderFilterSelections SortOrder { get; set; } = SearchSortOrderFilterSelections.Ascending;

    public Guid? UserId { get; set; }
}
