using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NodaTime;

namespace Application.Entities;

[Table("venue_invitations")]
public class VenueInvitationEntity : EntityBase
{
    [Column("email")]
    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public string EmailAddress { get; set; } = null!;

    [Column("venue_id")]
    [Required]
    public Guid VenueId { get; set; }

    [Column("role_id")]
    [Required]
    public Guid RoleId { get; set; }

    [Column("invited_by_user_id")]
    [Required]
    public Guid InvitedByUserId { get; set; }

    [Column("expires_at")]
    public Instant ExpiresAt { get; set; }

    [Column("accepted_at")]
    public Instant? AcceptedAt { get; set; }

    [Column("accepted_by_user_id")]
    public Guid? AcceptedByUserId { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [NotMapped]
    public bool IsAccepted => AcceptedAt.HasValue && AcceptedByUserId.HasValue;

    public VenueEntity Venue { get; set; } = null!;
    public VenueRoleEntity Role { get; set; } = null!;
    public UserEntity InvitedByUser { get; set; } = null!;
    public UserEntity? AcceptedByUser { get; set; }


    public virtual bool IsValid(IClock clock) => IsActive && !AcceptedAt.HasValue && ExpiresAt > clock.GetCurrentInstant();
}
