namespace Application.Queries;

public class GetUserVenueRoleQuery
{
    public Guid UserId { get; set; }
    public Guid VenueId { get; set; }
}