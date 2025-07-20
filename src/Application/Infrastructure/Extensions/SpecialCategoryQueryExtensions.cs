using Application.Domain.Entities;

namespace Application.Infrastructure.Extensions;

public static class SpecialCategoryQueryExtensions
{
    public static IQueryable<SpecialCategoryEntity> OrderedBySortOrder(
        this IQueryable<SpecialCategoryEntity> query)
    {
        return query.OrderBy(c => c.SortOrder);
    }
}