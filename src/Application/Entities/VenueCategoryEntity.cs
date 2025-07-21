using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities;

[Table("venue_categories")]
public class VenueCategoryEntity : EntityBase
{
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

    public List<VenueEntity> PrimaryVenues { get; set; } = new List<VenueEntity>();
    public List<VenueEntity> SecondaryVenues { get; set; } = new List<VenueEntity>();
}
