namespace Application.Queries;

public class UserRolesByVenueQuery
{
    public Guid VenueId { get; set; }
    public bool ActiveOnly { get; set; } = true;
}