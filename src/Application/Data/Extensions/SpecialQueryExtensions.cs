using Application.Data.Queries;
using Application.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Data.Extensions;

public static class SpecialQueryExtensions
{
    public static IQueryable<SpecialEntity> ByMenu(
        this IQueryable<SpecialEntity> query,
        Action<SpecialsByMenuQuery> configureQuery)
    {
        var searchQuery = new SpecialsByMenuQuery();
        configureQuery(searchQuery);

        query = query
            .Include(s => s.Category)
            .Where(s => s.SpecialMenuId == searchQuery.SpecialMenuId);

        if (!searchQuery.IncludeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return query.OrderBy(s => s.Category.SortOrder).ThenBy(s => s.Description);
    }

    public static Task<int> GetCountAsync(
        this IQueryable<SpecialEntity> query,
        Action<SpecialsCountQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new SpecialsCountQuery();
        configureQuery(searchQuery);

        if (searchQuery.SpecialMenuId.HasValue)
        {
            query = query.Where(s => s.SpecialMenuId == searchQuery.SpecialMenuId);
        }

        if (searchQuery.VenueId.HasValue)
        {
            query = query
                .Include(s => s.SpecialMenu)
                .Where(s => s.SpecialMenu!.VenueId == searchQuery.VenueId);
        }

        if (searchQuery.UserId.HasValue)
        {
            query = query
                .Include(s => s.SpecialMenu)
                    .ThenInclude(sm => sm!.Venue)
                        .ThenInclude(v => v.VenueUsers)
                .Where(s => s.SpecialMenu!.Venue.VenueUsers.Any(vu =>
                    vu.UserId == searchQuery.UserId.Value && vu.IsActive));
        }

        if (searchQuery.ActiveOnly)
        {
            query = query.Where(s => s.IsActive);
        }

        return query.CountAsync(cancellationToken);
    }

    public static IQueryable<SpecialEntity> ForAnalytics(
        this IQueryable<SpecialEntity> query,
        Action<SpecialsForAnalyticsQuery> configureQuery)
    {
        var searchQuery = new SpecialsForAnalyticsQuery();
        configureQuery(searchQuery);

        if (searchQuery.UserId.HasValue)
        {
            query = query
                .Include(s => s.SpecialMenu)
                    .ThenInclude(sm => sm!.Venue)
                        .ThenInclude(v => v.VenueUsers)
                .Where(s => s.SpecialMenu!.Venue.VenueUsers.Any(vu =>
                    vu.UserId == searchQuery.UserId.Value && vu.IsActive));
        }

        return query;
    }
}