namespace Application.Domain.Queries;

public class VenueUserRolesByVenueQuery
{
    public Guid VenueId { get; set; }
    public bool ActiveOnly { get; set; } = true;
}