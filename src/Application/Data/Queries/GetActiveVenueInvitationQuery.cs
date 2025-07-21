using NodaTime;

namespace Application.Data.Queries;

public class GetActiveVenueInvitationQuery
{
    public string EmailAddress { get; set; } = null!;
    public Guid VenueId { get; set; }
    public Instant CurrentInstant { get; set; }
}