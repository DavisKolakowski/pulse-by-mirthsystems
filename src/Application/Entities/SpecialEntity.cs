using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities;

[Table("specials")]
public class SpecialEntity : EntityBase
{
    [Column("special_category_id")]
    public Guid SpecialCategoryId { get; set; }

    [Column("description")]
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = null!; // e.g., "Half-Price Wings"

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("special_menu_id")]
    public Guid SpecialMenuId { get; set; }

    [Column("additional_data", TypeName = "jsonb")]
    public string? AdditionalData { get; set; } // JSONB for category-specific details, e.g., {"items": [{"name": "Wings", "price": 5.99}]}

    public SpecialCategoryEntity Category { get; set; } = null!;
    public SpecialMenuEntity? SpecialMenu { get; set; }
}
