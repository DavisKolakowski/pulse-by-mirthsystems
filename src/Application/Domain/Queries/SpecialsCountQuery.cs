namespace Application.Domain.Queries;

public class SpecialsCountQuery
{
    public Guid? SpecialMenuId { get; set; }
    public Guid? VenueId { get; set; }
    public Guid? UserId { get; set; }
    public bool ActiveOnly { get; set; } = false;
}