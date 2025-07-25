﻿using Application.Entities;

namespace Application.Data.Extensions;

public static class SpecialCategoryQueryExtensions
{
    public static IQueryable<SpecialCategoryEntity> OrderedBySortOrder(
        this IQueryable<SpecialCategoryEntity> query)
    {
        return query.OrderBy(c => c.SortOrder);
    }
}