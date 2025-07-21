using NetTopologySuite.Geometries;

namespace Application.Data.Queries;

public class VenuesWithinAreaQuery
{
    public Geometry SearchArea { get; set; } = null!;
}