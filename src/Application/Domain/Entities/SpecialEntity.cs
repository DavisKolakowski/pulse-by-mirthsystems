using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;

namespace Application.Domain.Entities;

[Table("specials")]
public class SpecialEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("venue_id")]
    public long VenueId { get; set; }

    [Column("special_category_id")]
    public int SpecialCategoryId { get; set; }

    [Column("description")]
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = null!; // e.g., "Half-Price Wings"

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("special_menu_id")]
    public long? SpecialMenuId { get; set; }

    [Column("additional_data", TypeName = "jsonb")]
    public string? AdditionalData { get; set; } // JSONB for category-specific details, e.g., {"items": [{"name": "Wings", "price": 5.99}]}

    public VenueEntity Venue { get; set; } = null!;
    public SpecialCategoryEntity Category { get; set; } = null!;
    public SpecialMenuEntity? SpecialMenu { get; set; }
}
