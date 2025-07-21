using Application.Entities;

namespace Application.Data.Extensions;

public static class VenueCategoryQueryExtensions
{
    public static IQueryable<VenueCategoryEntity> OrderedBySortOrder(
        this IQueryable<VenueCategoryEntity> query)
    {
        return query.OrderBy(c => c.SortOrder);
    }
}