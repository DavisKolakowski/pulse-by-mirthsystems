using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Domain.Entities;

[Table("venue_categories")]
public class VenueCategoryEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Column("icon")]
    [MaxLength(10)]
    public string? Icon { get; set; }

    [Column("sort_order")]
    public int SortOrder { get; set; }

    public List<VenueEntity> Venues { get; set; } = new List<VenueEntity>();
}
