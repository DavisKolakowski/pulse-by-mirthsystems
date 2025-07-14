using Microsoft.EntityFrameworkCore;
using Application.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Application.Infrastructure.Data.Context;

public sealed class ApplicationDbContext : IdentityDbContext<
    UserEntity,
    RoleEntity,
    long,
    UserClaimEntity,
    UserRoleEntity,
    UserLoginEntity,
    RoleClaimEntity,
    UserTokenEntity>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<VenueEntity> Venues => Set<VenueEntity>();
    public DbSet<VenueCategoryEntity> VenueCategories => Set<VenueCategoryEntity>();
    public DbSet<BusinessHoursEntity> BusinessHours => Set<BusinessHoursEntity>();
    public DbSet<DayOfWeekEntity> DaysOfWeek => Set<DayOfWeekEntity>();

    // Special entities
    public DbSet<SpecialEntity> Specials => Set<SpecialEntity>();
    public DbSet<SpecialCategoryEntity> SpecialCategories => Set<SpecialCategoryEntity>();
    public DbSet<SpecialMenuEntity> SpecialMenus => Set<SpecialMenuEntity>();
    public DbSet<SpecialMenuScheduleEntity> SpecialMenuSchedules => Set<SpecialMenuScheduleEntity>();

    // Venue Authorization entities
    public DbSet<VenueRoleEntity> VenueRoles => Set<VenueRoleEntity>();
    public DbSet<VenueUserRoleEntity> VenueUserRoles => Set<VenueUserRoleEntity>();
    public DbSet<VenueInvitationEntity> VenueInvitations => Set<VenueInvitationEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("address_standardizer");
        builder.HasPostgresExtension("address_standardizer_data_us");
        builder.HasPostgresExtension("fuzzystrmatch");
        builder.HasPostgresExtension("plpgsql");
        builder.HasPostgresExtension("postgis");
        builder.HasPostgresExtension("postgis_raster");
        builder.HasPostgresExtension("postgis_sfcgal");
        builder.HasPostgresExtension("postgis_tiger_geocoder");
        builder.HasPostgresExtension("postgis_topology");

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
