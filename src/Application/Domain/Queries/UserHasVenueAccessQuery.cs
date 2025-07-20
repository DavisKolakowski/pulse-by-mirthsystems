namespace Application.Domain.Queries;

public class UserHasVenueAccessQuery
{
    public Guid VenueId { get; set; }
    public Guid UserId { get; set; }
}