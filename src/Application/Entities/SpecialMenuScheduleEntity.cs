using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Application.Enums;
using Application.Models.Schedules;

using NodaTime;

namespace Application.Entities;

[Table("special_menu_schedules")]
public class SpecialMenuScheduleEntity : EntityBase
{
    [Column("menu_id")]
    [Required]
    public Guid MenuId { get; set; }

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

    [Column("recurrence")]
    [Required]
    public Recurrence Recurrence { get; set; }

    [Column("description")]
    [Required]
    public string Description { get; set; } = null!;

    [Column("cron_pattern", TypeName = "jsonb")]
    public CronPattern CronPattern { get; set; } = null!;

    [Column("status")]
    [Required]
    public SpecialMenuScheduleStatus Status { get; set; }

    public SpecialMenuEntity Menu { get; set; } = null!;
}
