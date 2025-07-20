namespace Application.Domain.Queries;

public class VenueUserRolesByUserQuery
{
    public Guid UserId { get; set; }
    public bool ActiveOnly { get; set; } = true;
}