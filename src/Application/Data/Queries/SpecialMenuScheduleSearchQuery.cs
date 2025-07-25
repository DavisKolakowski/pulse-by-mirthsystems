using Application.Enums;

using NetTopologySuite.Geometries;

using NodaTime;

namespace Application.Data.Queries;

public class SpecialMenuScheduleSearchQuery
{
    public Geometry SearchArea { get; set; } = null!;

    public string? SearchTerm { get; set; }
    public List<Guid> SpecialCategoryIds { get; set; } = new List<Guid>();
    public List<Guid> VenueCategoryIds { get; set; } = new List<Guid>();

    public SearchSpecialMenusSortByFilterSelections SortBy { get; set; } = SearchSpecialMenusSortByFilterSelections.Distance;
    public SearchSortOrderFilterSelections SortOrder { get; set; } = SearchSortOrderFilterSelections.Ascending;
}
