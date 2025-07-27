using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Application.Enums;
using Application.Models.Schedules;

using NodaTime;

namespace Application.Entities;

[Table("special_menu_schedules")]
public class SpecialMenuScheduleEntity : EntityBase
{
    [Column("special_menu_id")]
    [Required]
    public Guid SpecialMenuId { get; set; }

    [Column("start_date")]
    [Required]
    public OffsetDate StartDate { get; set; }

    [Column("start_time")]
    [Required]
    public OffsetTime StartTime { get; set; }

    [Column("end_time")]
    [Required]
    public OffsetTime EndTime { get; set; }

    [Column("expiration_date")]
    public OffsetDate? ExpirationDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("recurrence_settings", TypeName = "jsonb")]
    [Required]
    public RecurrenceSettings RecurrenceSettings { get; set; } = null!;

    [Column("status")]
    [Required]
    public SpecialMenuScheduleStatus Status { get; set; } = SpecialMenuScheduleStatus.Scheduled;

    public SpecialMenuEntity SpecialMenu { get; set; } = null!;
}
