using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Application.Domain.Common;

using NodaTime;

namespace Application.Domain.Entities;

[Table("special_menu_schedules")]
public class SpecialMenuScheduleEntity : EntityBase
{
    [Column("special_menu_id")]
    [Required]
    public Guid SpecialMenuId { get; set; }

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

    [Column("recurrence_pattern", TypeName = "jsonb")]
    [Required]
    public RecurrencePattern RecurrencePattern { get; set; } = null!;

    public SpecialMenuEntity SpecialMenu { get; set; } = null!;
}
