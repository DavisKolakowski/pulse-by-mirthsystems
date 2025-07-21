using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities;

[Table("special_categories")]
public class SpecialCategoryEntity : EntityBase
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

    public List<SpecialEntity> Specials { get; set; } = new List<SpecialEntity>();
}

