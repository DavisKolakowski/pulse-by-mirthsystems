using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities;

[Table("days_of_week")]
public class DayOfWeekEntity : EntityBase
{
    [Column("name")]
    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = null!;

    [Column("short_name")]
    [Required]
    [MaxLength(3)]
    public string ShortName { get; set; } = null!;

    [Column("iso_number")]
    public int IsoNumber { get; set; }

    [Column("is_weekday")]
    public bool IsWeekday { get; set; }

    [Column("sort_order")]
    public int SortOrder { get; set; }

    public List<VenueBusinessHoursEntity> BusinessHours { get; set; } = new List<VenueBusinessHoursEntity>();
}
