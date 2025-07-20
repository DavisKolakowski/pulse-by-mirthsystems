using Application.Enums;

using NetTopologySuite.Geometries;

using NodaTime;

namespace Application.Domain.Queries;

public class SpecialMenuSearchQuery : PagedQuery
{
    public required Geometry SearchArea { get; set; }

    public string? SearchTerm { get; set; }
    public LocalDate? SearchDate { get; set; }
    public LocalTime? SearchTime { get; set; }

    public List<Guid> SpecialCategoryIds { get; set; } = new List<Guid>();
    public List<Guid> VenueCategoryIds { get; set; } = new List<Guid>();
    public bool CurrentlyRunning { get; set; } = true;

    public SearchSpecialMenusSortByFilterSelections SortBy { get; set; } = SearchSpecialMenusSortByFilterSelections.Distance;
    public SearchSortOrderFilterSelections SortOrder { get; set; } = SearchSortOrderFilterSelections.Ascending;
}
