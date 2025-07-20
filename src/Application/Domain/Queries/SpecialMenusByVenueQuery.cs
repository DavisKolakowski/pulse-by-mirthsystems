namespace Application.Domain.Queries;

public class SpecialMenusByVenueQuery
{
    public Guid VenueId { get; set; }
    public bool IncludeInactive { get; set; } = false;
}