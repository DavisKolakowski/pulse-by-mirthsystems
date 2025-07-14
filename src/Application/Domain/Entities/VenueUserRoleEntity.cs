using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Application.Domain.Entities;

[Table("venue_user_roles")]
public class VenueUserRoleEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("user_id")]
    [Required]
    public long UserId { get; set; }

    [Column("venue_id")]
    [Required]
    public long VenueId { get; set; }

    [Column("role_id")]
    [Required]
    public int RoleId { get; set; }

    [Column("granted_by_user_id")]
    [Required]
    public long GrantedByUserId { get; set; }

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
