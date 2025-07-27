using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Models;
using Application.Models.Requests;
using Application.Models.Specials;

namespace Application.Contracts.Services;


public interface ISpecialMenuService
{
    Task<PagedList<SpecialMenuScheduleSearchItem>> SearchSpecialsAsync(SpecialsSearch filters, CancellationToken cancellationToken = default);
}
