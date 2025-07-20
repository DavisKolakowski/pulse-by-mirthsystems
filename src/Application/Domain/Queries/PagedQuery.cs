using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Queries;

public abstract class PagedQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    public void Validate()
    {
        if (Page < 1)
        {
            Page = 1;
        }
        if (PageSize < 1)
        {
            PageSize = 20;
        }
        if (PageSize > 200)
        {
            PageSize = 200;
        }
    }
}
