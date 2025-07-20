using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Domain.Common;
using Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Extensions;

public static class QueryExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
        where T : EntityBase
    {
        if (page < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be at least 1.");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be at least 1.");
        }

        var totalItems = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, page, pageSize, totalItems);
    }

    public static PagedList<T> ToPagedList<T>(
        this IQueryable<T> query,
        int page,
        int pageSize)
        where T : EntityBase
    {
        if (page < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be at least 1.");
        }
        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be at least 1.");
        }

        var totalItems = query.Count();
        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, page, pageSize, totalItems);
    }
}
