using Application.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Data.Extensions;

public static class DayOfWeekQueryExtensions
{
    public static Task<DayOfWeekEntity?> GetByIsoNumberAsync(
        this IQueryable<DayOfWeekEntity> query,
        int isoNumber,
        CancellationToken cancellationToken = default)
    {
        return query.FirstOrDefaultAsync(d => d.IsoNumber == isoNumber, cancellationToken);
    }

    public static IQueryable<DayOfWeekEntity> OrderedBySortOrder(
        this IQueryable<DayOfWeekEntity> query)
    {
        return query.OrderBy(d => d.SortOrder);
    }
}