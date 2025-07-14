using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Domain.Entities;

[Table("venue_roles")]
public class VenueRoleEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public string Name { get; set; } = null!; // "venue-owner" | "venue-manager" | "venue-staff" 

    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    public List<VenueUserRoleEntity> VenueUsers { get; set; } = new List<VenueUserRoleEntity>();
    public List<VenueInvitationEntity> VenueInvitations { get; set; } = new List<VenueInvitationEntity>();
}
