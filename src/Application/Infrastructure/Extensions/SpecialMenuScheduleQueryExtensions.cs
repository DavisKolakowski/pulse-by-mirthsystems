using Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using NodaTime;

namespace Application.Infrastructure.Extensions;

public static class SpecialMenuScheduleQueryExtensions
{
    public static IQueryable<SpecialMenuScheduleEntity> ByMenu(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Guid specialMenuId,
        bool includeInactive = false)
    {
        query = query.Where(s => s.SpecialMenuId == specialMenuId);

        if (!includeInactive)
        {
            query = query.Where(s => s.IsActive);
        }

        return query;
    }

    public static IQueryable<SpecialMenuScheduleEntity> ByVenue(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Guid venueId)
    {
        return query
            .Include(s => s.SpecialMenu)
            .Where(s => s.SpecialMenu.VenueId == venueId);
    }

    public static IQueryable<SpecialMenuScheduleEntity> ForAnalytics(
        this IQueryable<SpecialMenuScheduleEntity> query,
        Guid? userId = null)
    {
        query = query
            .Include(s => s.SpecialMenu)
                .ThenInclude(sm => sm.Venue)
                    .ThenInclude(v => v.VenueUsers)
            .Where(s => s.IsActive);

        if (userId.HasValue)
        {
            query = query.Where(s => s.SpecialMenu.Venue.VenueUsers.Any(vu =>
                vu.UserId == userId.Value && vu.IsActive));
        }

        return query;
    }
}