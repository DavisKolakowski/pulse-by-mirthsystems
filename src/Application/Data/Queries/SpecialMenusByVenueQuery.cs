namespace Application.Data.Queries;

public class SpecialMenusByVenueQuery
{
    public Guid VenueId { get; set; }
    public bool IncludeInactive { get; set; } = false;
}