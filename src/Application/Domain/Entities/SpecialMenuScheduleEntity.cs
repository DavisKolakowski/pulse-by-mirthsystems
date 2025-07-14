using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NodaTime;

namespace Application.Domain.Entities;

[Table("special_menu_schedules")]
public class SpecialMenuScheduleEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("special_menu_id")]
    [Required]
    public long SpecialMenuId { get; set; }

    [Column("start_date")]
    [Required]
    public LocalDate StartDate { get; set; }

    [Column("start_time")]
    [Required]
    public LocalTime StartTime { get; set; }

    [Column("end_time")]
    [Required]
    public LocalTime EndTime { get; set; }

    [Column("expiration_date")]
    public LocalDate? ExpirationDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("is_recurring")]
    public bool IsRecurring { get; set; } = true;

    [Column("recurrence_pattern")]
    [Required]
    [MaxLength(100)]
    public string RecurrencePattern { get; set; } = null!; // e.g., "0 16 * * 1-4" for 4:00 PM Mon-Thu

    public SpecialMenuEntity SpecialMenu { get; set; } = null!;
}
