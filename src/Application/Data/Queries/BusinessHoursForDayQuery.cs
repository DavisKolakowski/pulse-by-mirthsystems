namespace Application.Data.Queries;

public class BusinessHoursForDayQuery
{
    public Guid VenueId { get; set; }
    public Guid? DayOfWeekId { get; set; }
    public int? IsoNumber { get; set; }
}