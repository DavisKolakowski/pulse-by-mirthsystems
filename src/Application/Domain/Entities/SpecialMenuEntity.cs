using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Domain.Entities;

[Table("special_menus")]
public class SpecialMenuEntity : EntityBase
{
    [Column("venue_id")]
    public Guid VenueId { get; set; }

    [Column("name")]
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [MaxLength(500)]
    public string? Description { get; set; }

    public VenueEntity Venue { get; set; } = null!;
    public List<SpecialMenuScheduleEntity> Schedules { get; set; } = new List<SpecialMenuScheduleEntity>();
    public List<SpecialEntity> Specials { get; set; } = new List<SpecialEntity>();
}
