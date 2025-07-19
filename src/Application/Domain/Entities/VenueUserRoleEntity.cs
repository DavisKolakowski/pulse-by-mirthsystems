using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Application.Domain.Entities;

[Table("venue_user_roles")]
public class VenueUserRoleEntity : EntityBase
{

    [Column("user_id")]
    [Required]
    public Guid UserId { get; set; }

    [Column("venue_id")]
    [Required]
    public Guid VenueId { get; set; }

    [Column("role_id")]
    [Required]
    public Guid RoleId { get; set; }

    [Column("granted_by_user_id")]
    [Required]
    public Guid GrantedByUserId { get; set; }

    [Column("granted_at")]
    public Instant GrantedAt { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("notes")]
    [MaxLength(500)]
    public string? Notes { get; set; }

    public VenueEntity Venue { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public VenueRoleEntity Role { get; set; } = null!;
    public UserEntity GrantedByUser { get; set; } = null!;
}
