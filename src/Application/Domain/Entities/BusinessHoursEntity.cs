using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NodaTime;

namespace Application.Domain.Entities;

[Table("business_hours")]
public class BusinessHoursEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("venue_id")]
    public long VenueId { get; set; }

    [Column("day_of_week_id")]
    public byte DayOfWeekId { get; set; }

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
