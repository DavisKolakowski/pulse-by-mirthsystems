using NodaTime;

namespace Application.Domain.Queries;

public class VenueInvitationsByVenueQuery
{
    public Guid VenueId { get; set; }
    public bool IncludeExpired { get; set; } = false;
    public Instant? CurrentInstant { get; set; }
}