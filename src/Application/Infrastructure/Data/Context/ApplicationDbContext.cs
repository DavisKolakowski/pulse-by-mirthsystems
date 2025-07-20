using Application.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using NodaTime;

namespace Application.Infrastructure.Data.Context;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IClock _clock;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IClock clock) : base(options) 
    {
        this._clock = clock;
    }

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
    public DbSet<UserEntity> Users => Set<UserEntity>();
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

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = this._clock.GetCurrentInstant();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = this._clock.GetCurrentInstant();
                    break;
            }
        }
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = this._clock.GetCurrentInstant();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = this._clock.GetCurrentInstant();
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
