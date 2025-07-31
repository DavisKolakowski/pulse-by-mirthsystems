using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Infrastructure.Data.Queries;

public class PagedQuery
{
    PagedQuery()
    {
        Validate();
    }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    private void Validate()
    {
        if (PageNumber < 1)
        {
            PageNumber = 1;
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
