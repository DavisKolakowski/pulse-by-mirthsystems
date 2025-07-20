using Application.Domain.Entities;
using Application.Domain.Queries;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Extensions;

public static class SpecialMenuScheduleQueryExtensions
{
    public static IQueryable<SpecialMenuScheduleEntity> ByMenu(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<SchedulesByMenuQuery> configureQuery)
    {
        var searchQuery = new SchedulesByMenuQuery();
        configureQuery(searchQuery);

        query = query.Where(s => s.SpecialMenuId == searchQuery.SpecialMenuId);

        if (!searchQuery.IncludeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return query;
    }

    public static IQueryable<SpecialMenuScheduleEntity> ByVenue(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<SchedulesByVenueQuery> configureQuery)
    {
        var searchQuery = new SchedulesByVenueQuery();
        configureQuery(searchQuery);

        return query
            .Include(s => s.SpecialMenu)
            .Where(s => s.SpecialMenu.VenueId == searchQuery.VenueId);
    }

    public static IQueryable<SpecialMenuScheduleEntity> ForAnalytics(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Action<SchedulesForAnalyticsQuery> configureQuery)
    {
        var searchQuery = new SchedulesForAnalyticsQuery();
        configureQuery(searchQuery);

        query = query
            .Include(s => s.SpecialMenu)
                .ThenInclude(sm => sm.Venue)
            .Where(s => s.IsActive);

        if (searchQuery.UserId.HasValue)
        {
            query = query
                .Include(s => s.SpecialMenu.Venue.VenueUsers)
                .Where(s => s.SpecialMenu.Venue.VenueUsers.Any(vu =>
                    vu.UserId == searchQuery.UserId.Value && vu.IsActive));
        }

        return query;
    }
}