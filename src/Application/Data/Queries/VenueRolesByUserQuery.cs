namespace Application.Data.Queries;

public class VenueRolesByUserQuery
{
    public Guid UserId { get; set; }
    public bool ActiveOnly { get; set; } = true;
}