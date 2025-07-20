using Application.Domain.Entities;
using Application.Domain.Queries;
using Application.Enums;

using Microsoft.EntityFrameworkCore;

using NetTopologySuite.Geometries;

namespace Application.Infrastructure.Extensions;

public static class VenueQueryExtensions
{
    public static IQueryable<VenueEntity> Search(
        this IQueryable<VenueEntity> query,
        Action<VenueSearchQuery> configureQuery)
    {
        var searchQuery = new VenueSearchQuery();
        configureQuery(searchQuery);

        return query
            .Include(v => v.PrimaryCategory)
            .Include(v => v.SecondaryCategory)
            .Include(v => v.SpecialMenus)
                .ThenInclude(sm => sm.Specials)
            .ApplyFilters(searchQuery)
            .ApplySorting(searchQuery.SortBy, searchQuery.SortOrder);
    }

    public static IQueryable<VenueEntity> WithinArea(
        this IQueryable<VenueEntity> query,
        VenuesWithinAreaQuery searchQuery)
    {
        return query.Where(v => v.Location.Within(searchQuery.SearchArea));
    }

    public static IQueryable<VenueEntity> ByUser(
        this IQueryable<VenueEntity> query,
        VenuesByUserQuery searchQuery)
    {
        return query.Where(v => v.VenueUsers.Any(vu =>
            vu.UserId == searchQuery.UserId &&
            vu.IsActive));
    }

    public static async Task<bool> UserHasAccessAsync(
        this IQueryable<VenueEntity> query,
        UserHasVenueAccessQuery searchQuery,
        CancellationToken cancellationToken = default)
    {
        return await query
            .Where(v => v.Id == searchQuery.VenueId)
            .AnyAsync(v => v.VenueUsers.Any(vu =>
                vu.UserId == searchQuery.UserId &&
                vu.IsActive),
                cancellationToken);
    }

    public static IQueryable<VenueEntity> WithFullDetails(
        this IQueryable<VenueEntity> query)
    {
        return query
            .Include(v => v.PrimaryCategory)
            .Include(v => v.SecondaryCategory)
            .Include(v => v.BusinessHours)
                .ThenInclude(bh => bh.DayOfWeek)
            .Include(v => v.SpecialMenus)
                .ThenInclude(sm => sm.Schedules)
            .Include(v => v.SpecialMenus)
                .ThenInclude(sm => sm.Specials)
                    .ThenInclude(s => s.Category);
    }

    public static async Task<VenueEntity?> GetWithDetailsAsync(
        this IQueryable<VenueEntity> query,
        GetVenueWithDetailsQuery searchQuery,
        CancellationToken cancellationToken = default)
    {
        return await query
            .WithFullDetails()
            .FirstOrDefaultAsync(v => v.Id == searchQuery.VenueId, cancellationToken);
    }

    private static IQueryable<VenueEntity> ApplyFilters(
        this IQueryable<VenueEntity> query,
        VenueSearchQuery searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery.SearchTerm))
        {
            var searchTerm = searchQuery.SearchTerm.ToLower();
            query = query.Where(v =>
                v.Name.ToLower().Contains(searchTerm) ||
                (v.Description != null && v.Description.ToLower().Contains(searchTerm)) ||
                v.StreetAddress.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(searchQuery.Locality))
        {
            query = query.Where(v => v.Locality.ToLower().Contains(searchQuery.Locality.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(searchQuery.Region))
        {
            query = query.Where(v => v.Region == searchQuery.Region);
        }

        if (searchQuery.CategoryIds.Any())
        {
            query = query.Where(v =>
                searchQuery.CategoryIds.Contains(v.PrimaryCategoryId) ||
                (v.SecondaryCategoryId.HasValue && searchQuery.CategoryIds.Contains(v.SecondaryCategoryId.Value)));
        }

        query = searchQuery.Status switch
        {
            SearchVenuesStatusFilterSelections.Active => query.Where(v => v.IsActive),
            SearchVenuesStatusFilterSelections.Inactive => query.Where(v => !v.IsActive),
            _ => query
        };

        if (searchQuery.UserId.HasValue)
        {
            query = query.Where(v => v.VenueUsers.Any(vu =>
                vu.UserId == searchQuery.UserId.Value &&
                vu.IsActive));
        }

        return query;
    }

    private static IQueryable<VenueEntity> ApplySorting(
        this IQueryable<VenueEntity> query,
        SearchVenuesSortByFilterSelections sortBy,
        SearchSortOrderFilterSelections sortOrder)
    {
        return sortBy switch
        {
            SearchVenuesSortByFilterSelections.Name => sortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(v => v.Name)
                : query.OrderByDescending(v => v.Name),
            SearchVenuesSortByFilterSelections.CreatedAt => sortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(v => v.CreatedAt)
                : query.OrderByDescending(v => v.CreatedAt),
            SearchVenuesSortByFilterSelections.Locality => sortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(v => v.Locality)
                : query.OrderByDescending(v => v.Locality),
            SearchVenuesSortByFilterSelections.Region => sortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(v => v.Region)
                : query.OrderByDescending(v => v.Region),
            SearchVenuesSortByFilterSelections.Status => sortOrder == SearchSortOrderFilterSelections.Ascending
                ? query.OrderBy(v => v.IsActive)
                : query.OrderByDescending(v => v.IsActive),
            _ => query.OrderBy(v => v.Name)
        };
    }
}