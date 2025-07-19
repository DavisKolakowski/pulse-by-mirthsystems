using Application.Services.DatabaseInitializer.Models;

namespace Application.Services.DatabaseInitializer.Options;

public class DataOptions
{
    public const string ConfigurationSection = "Data";

    public List<DayOfWeekSeed> DaysOfWeek { get; set; } = new List<DayOfWeekSeed>();
    public List<SpecialCategorySeed> SpecialCategories { get; set; } = new List<SpecialCategorySeed>();
    public List<VenueCategorySeed> VenueCategories { get; set; } = new List<VenueCategorySeed>();
    public List<VenueRoleSeed> VenueRoles { get; set; } = new List<VenueRoleSeed>();
}