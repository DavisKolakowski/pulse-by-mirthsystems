using Application.Data.Queries;
using Application.Entities;
using Application.Enums;

using Microsoft.EntityFrameworkCore;

namespace Application.Data.Extensions;

public static class SpecialMenuQueryExtensions
{
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
}