namespace Application.Services.DatabaseInitializer.Models;

public class DayOfWeekSeed
{
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required int IsoNumber { get; set; }
    public required bool IsWeekday { get; set; }
    public required int SortOrder { get; set; }
}
