using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Common;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }

    public PagedList(IReadOnlyList<T> items, int currentPage, int pageSize, int totalItems)
    {
        Items = items;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
