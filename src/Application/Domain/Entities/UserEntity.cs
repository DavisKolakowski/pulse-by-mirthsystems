using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Application.Domain.Entities;

[Table("users")]
public class UserEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("sub")]
    [Required]
    [MaxLength(100)]
    public string Sub { get; set; } = null!; // Keycloak 'sub' claim (UUID string)

    [Column("username")]
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = null!;

    [Column("first_name")]
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Column("email_address")]
    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public string EmailAddress { get; set; } = null!;

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_at")]
    public Instant CreatedAt { get; set; }

    [Column("updated_at")]
    public Instant UpdatedAt { get; set; }

    [Column("last_login_at")]
    public Instant LastLoginAt { get; set; }


    public List<VenueUserRoleEntity> VenueRoles { get; set; } = new List<VenueUserRoleEntity>();
    public List<VenueInvitationEntity> SentInvitations { get; set; } = new List<VenueInvitationEntity>();
    public List<VenueInvitationEntity> ReceivedInvitations { get; set; } = new List<VenueInvitationEntity>();
}
