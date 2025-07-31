using Application.Entities;
using Application.Services.DatabaseInitializer.Models;

namespace Application.Services.DatabaseInitializer.Utilities;

public static class SeedMapper
{
    public static List<DayOfWeekEntity> MapDaysOfWeek(List<DayOfWeekSeed> seeds)
    {
        return seeds.Select(seed => new DayOfWeekEntity
        {
            Name = seed.Name,
            ShortName = seed.ShortName,
            IsoNumber = seed.IsoNumber,
            IsWeekday = seed.IsWeekday,
            SortOrder = seed.SortOrder
        }).ToList();
    }

    public static List<SpecialMenuItemGroupEntity> MapSpecialCategories(List<SpecialCategorySeed> seeds)
    {
        return seeds.Select(seed => new SpecialMenuItemGroupEntity
        {
            Name = seed.Name,
            Description = seed.Description,
            Icon = seed.Icon,
            SortOrder = seed.SortOrder
        }).ToList();
    }

    public static List<VenueCategoryEntity> MapVenueCategories(List<VenueCategorySeed> seeds)
    {
        return seeds.Select(seed => new VenueCategoryEntity
        {
            Name = seed.Name,
            Description = seed.Description,
            Icon = seed.Icon,
            SortOrder = seed.SortOrder
        }).ToList();
    }

    public static List<VenueRoleEntity> MapVenueRoles(List<VenueRoleSeed> seeds)
    {
        return seeds.Select(seed => new VenueRoleEntity
        {
            Name = seed.Name,
            Description = seed.Description
        }).ToList();
    }
}
