using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities;

[Table("special_menu_items")]
public class SpecialMenuItemEntity : EntityBase
{
    [Column("group_id")]
    public Guid GroupId { get; set; }

    [Column("description")]
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = null!; // e.g., "Half-Price Wings"

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("menu_id")]
    public Guid MenuId { get; set; }

    public SpecialMenuItemGroupEntity Group { get; set; } = null!;
    public SpecialMenuEntity? Menu { get; set; }
}
