using NetTopologySuite.Geometries;

namespace Application.Queries;

public class VenuesWithinAreaQuery
{
    public Geometry SearchArea { get; set; } = null!;
}