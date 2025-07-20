namespace Application.Domain.Queries;

public class UsersByVenueQuery
{
    public Guid VenueId { get; set; }
    public bool ActiveOnly { get; set; } = true;
}