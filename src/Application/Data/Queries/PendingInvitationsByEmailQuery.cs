using NodaTime;

namespace Application.Queries;

public class PendingInvitationsByEmailQuery
{
    public string EmailAddress { get; set; } = null!;
    public Instant CurrentInstant { get; set; }
}