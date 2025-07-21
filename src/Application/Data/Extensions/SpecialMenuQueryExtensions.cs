using Application.Data.Queries;
using Application.Entities;
using Application.Enums;

using Microsoft.EntityFrameworkCore;

namespace Application.Data.Extensions;

public static class SpecialMenuQueryExtensions
{
    public static IQueryable<SpecialMenuEntity> Search(
        this IQueryable<SpecialMenuEntity> query,
        Action<SpecialMenuSearchQuery> configureQuery)
    {
        var searchQuery = new SpecialMenuSearchQuery();
        configureQuery(searchQuery);
        query = query
            .Include(sm => sm.Venue)
            .Where(sm => sm.Venue.Location.Within(searchQuery.SearchArea));

        query = query
            .Include(sm => sm.Venue)
                .ThenInclude(v => v.PrimaryCategory)
            .Include(sm => sm.Venue)
                .ThenInclude(v => v.SecondaryCategory)
            .Include(sm => sm.Venue)
                .ThenInclude(v => v.BusinessHours)
                    .ThenInclude(bh => bh.DayOfWeek)
            .Include(sm => sm.Specials)
                .ThenInclude(s => s.Category)
            .Include(sm => sm.Schedules)
            .ApplyFilters(searchQuery);

        var centerPoint = searchQuery.SearchArea.Centroid;
        query = searchQuery.SortBy switch
        {
            SearchSpecialMenusSortByFilterSelections.Distance => searchQuery.SortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(sm => sm.Venue.Location.Distance(centerPoint))
                : query.OrderByDescending(sm => sm.Venue.Location.Distance(centerPoint)),
            SearchSpecialMenusSortByFilterSelections.ItemCount => searchQuery.SortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(sm => sm.Specials.Count(s => s.IsActive))
                : query.OrderByDescending(sm => sm.Specials.Count(s => s.IsActive)),
            _ => query.OrderBy(sm => sm.Venue.Location.Distance(centerPoint))
        };

        return query;
    }

    public static IQueryable<SpecialMenuEntity> ByVenue(
        this IQueryable<SpecialMenuEntity> query,
        Action<SpecialMenusByVenueQuery> configureQuery)
    {
        var searchQuery = new SpecialMenusByVenueQuery();
        configureQuery(searchQuery);

        query = query.Where(sm => sm.VenueId == searchQuery.VenueId);

        if (!searchQuery.IncludeInactive)
        {
            query = query.Where(sm => sm.Schedules.Any(s => s.IsActive));
        }

        return query
            .Include(sm => sm.Schedules)
            .Include(sm => sm.Specials)
                .ThenInclude(s => s.Category);
    }

    public static IQueryable<SpecialMenuEntity> WithDetails(
        this IQueryable<SpecialMenuEntity> query)
    {
        return query
            .Include(sm => sm.Venue)
            .Include(sm => sm.Schedules)
            .Include(sm => sm.Specials)
                .ThenInclude(s => s.Category);
    }

    public static Task<SpecialMenuEntity?> GetWithDetailsAsync(
        this IQueryable<SpecialMenuEntity> query,
        Action<GetSpecialMenuDetailsQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new GetSpecialMenuDetailsQuery();
        configureQuery(searchQuery);

        return query
            .WithDetails()
            .FirstOrDefaultAsync(sm => sm.Id == searchQuery.SpecialMenuId, cancellationToken);
    }

    private static IQueryable<SpecialMenuEntity> ApplyFilters(
        this IQueryable<SpecialMenuEntity> query,
        SpecialMenuSearchQuery searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery.SearchTerm))
        {
            var searchTerm = searchQuery.SearchTerm.ToLower();
            query = query.Where(sm =>
                sm.Name.ToLower().Contains(searchTerm) ||
                sm.Description != null && sm.Description.ToLower().Contains(searchTerm) ||
                sm.Specials.Any(s => s.Description.ToLower().Contains(searchTerm)));
        }

        if (searchQuery.SpecialCategoryIds.Any())
        {
            query = query.Where(sm =>
                sm.Specials.Any(s => searchQuery.SpecialCategoryIds.Contains(s.SpecialCategoryId)));
        }

        if (searchQuery.VenueCategoryIds.Any())
        {
            query = query.Where(sm =>
                searchQuery.VenueCategoryIds.Contains(sm.Venue.PrimaryCategoryId) ||
                sm.Venue.SecondaryCategoryId.HasValue &&
                 searchQuery.VenueCategoryIds.Contains(sm.Venue.SecondaryCategoryId.Value));
        }

        query = query.Where(sm => sm.Schedules.Any(s => s.IsActive));

        return query;
    }
}