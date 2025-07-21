using NodaTime;

namespace Application.Data.Queries;

public class VenueInvitationsByVenueQuery
{
    public Guid VenueId { get; set; }
    public bool IncludeExpired { get; set; } = false;
    public Instant? CurrentInstant { get; set; }
}