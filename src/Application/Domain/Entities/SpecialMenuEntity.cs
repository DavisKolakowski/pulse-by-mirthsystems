using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NodaTime;

namespace Application.Domain.Entities;

[Table("special_menus")]
public class SpecialMenuEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("venue_id")]
    public long VenueId { get; set; }

    [Column("name")]
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [MaxLength(500)]
    public string? Description { get; set; }

    [Column("created_at")]
    public Instant CreatedAt { get; set; }

    [Column("updated_at")]
    public Instant? UpdatedAt { get; set; }

    public VenueEntity Venue { get; set; } = null!;
    public List<SpecialMenuScheduleEntity> Schedules { get; set; } = new List<SpecialMenuScheduleEntity>();
    public List<SpecialEntity> Specials { get; set; } = new List<SpecialEntity>();
}
