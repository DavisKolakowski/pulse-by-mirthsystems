using NetTopologySuite.Geometries;

namespace Application.Domain.Queries;

public class VenuesWithinAreaQuery
{
    public Geometry SearchArea { get; set; } = null!;
}