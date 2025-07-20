using Application.Domain.Entities;
using Application.Domain.Queries;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Extensions;

public static class BusinessHoursQueryExtensions
{
    public static IQueryable<BusinessHoursEntity> ByVenue(
        this IQueryable<BusinessHoursEntity> query,
        Action<BusinessHoursByVenueQuery> configureQuery)
    {
        var searchQuery = new BusinessHoursByVenueQuery();
        configureQuery(searchQuery);

        return query
            .Include(bh => bh.DayOfWeek)
            .Where(bh => bh.VenueId == searchQuery.VenueId)
            .OrderBy(bh => bh.DayOfWeek.SortOrder);
    }

    public static Task<BusinessHoursEntity?> GetForDayAsync(
        this IQueryable<BusinessHoursEntity> query,
        Action<BusinessHoursForDayQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new BusinessHoursForDayQuery();
        configureQuery(searchQuery);

        if (searchQuery.DayOfWeekId.HasValue)
        {
            return query
                .Include(bh => bh.DayOfWeek)
                .FirstOrDefaultAsync(bh =>
                    bh.VenueId == searchQuery.VenueId &&
                    bh.DayOfWeekId == searchQuery.DayOfWeekId.Value,
                    cancellationToken);
        }
        else if (searchQuery.IsoNumber.HasValue)
        {
            return query
                .Include(bh => bh.DayOfWeek)
                .FirstOrDefaultAsync(bh =>
                    bh.VenueId == searchQuery.VenueId &&
                    bh.DayOfWeek.IsoNumber == searchQuery.IsoNumber.Value,
                    cancellationToken);
        }

        return Task.FromResult<BusinessHoursEntity?>(null);
    }
}