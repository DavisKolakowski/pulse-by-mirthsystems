using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

using NodaTime;

namespace Application.Entities;

[Table("business_hours")]
public class BusinessHoursEntity : EntityBase
{
    [Column("venue_id")]
    public Guid VenueId { get; set; }

    [Column("day_of_week_id")]
    public Guid DayOfWeekId { get; set; }

    [Column("open_time")]
    public LocalTime? OpenTime { get; set; }

    [Column("close_time")]
    public LocalTime? CloseTime { get; set; }

    [Column("is_closed")]
    [DefaultValue(false)]
    public bool IsClosed { get; set; }

    public VenueEntity Venue { get; set; } = null!;
    public DayOfWeekEntity DayOfWeek { get; set; } = null!;
}
