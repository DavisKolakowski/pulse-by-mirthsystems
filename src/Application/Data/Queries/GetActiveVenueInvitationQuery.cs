using NodaTime;

namespace Application.Queries;

public class GetActiveVenueInvitationQuery
{
    public string EmailAddress { get; set; } = null!;
    public Guid VenueId { get; set; }
    public Instant CurrentInstant { get; set; }
}