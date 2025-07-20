using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Enums;

namespace Application.Domain.Queries;

public class VenueSearchQuery : PagedQuery
{
    public string? SearchTerm { get; set; }

    public string? Locality { get; set; }
    public string? Region { get; set; }

    public List<Guid> CategoryIds { get; set; } = new List<Guid>();

    public VenueStatusFilterSelections Status { get; set; } = VenueStatusFilterSelections.Active;

    public Guid? UserId { get; set; }
}
