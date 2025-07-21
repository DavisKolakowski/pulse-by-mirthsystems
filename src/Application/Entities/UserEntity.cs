using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NodaTime;

namespace Application.Entities;

[Table("users")]
public class UserEntity : EntityBase
{
    [Column("name_identifier")]
    [Required]
    [MaxLength(100)]
    public string NameIdentifier { get; set; } = null!; // Keycloak 'sub' claim (UUID string)

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

    [Column("last_login_at")]
    public Instant LastLoginAt { get; set; }


    public List<VenueUserRoleEntity> VenueRoles { get; set; } = new List<VenueUserRoleEntity>();
    public List<VenueInvitationEntity> SentInvitations { get; set; } = new List<VenueInvitationEntity>();
    public List<VenueInvitationEntity> ReceivedInvitations { get; set; } = new List<VenueInvitationEntity>();
}
