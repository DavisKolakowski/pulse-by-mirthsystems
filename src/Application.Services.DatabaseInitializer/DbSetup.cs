using System;
using System.Threading;

using Application.Data;
using Application.Domain.Entities;
using Application.Services.DatabaseInitializer.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace Application.Services.DatabaseInitializer;

public class DbSetup
{
    private readonly ILogger<DbSetup> _logger;

    public DbSetup(ILogger<DbSetup> logger)
    {
        this._logger = logger;
    }

    public async Task RunAsync(ApplicationDbContext dbContext, IOptions<DataOptions> options, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting database setup process.");

        await EnsureDatabaseAsync(dbContext, cancellationToken);
        await RunMigrationAsync(dbContext, cancellationToken);

        var dataOptions = options.Value;
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Starting data seeding.");
            await SeedDataAsync(dbContext, dataOptions, cancellationToken);
            _logger.LogInformation("Data seeding completed.");
        });

        _logger.LogInformation("Database setup process completed.");
    }

    private async Task SeedDataAsync(ApplicationDbContext dbContext, DataOptions dataOptions, CancellationToken cancellationToken)
    {
        if (dataOptions.DaysOfWeek.Count > 0)
        {
            _logger.LogDebug("{Count} Days Of Week found in default data settings.", dataOptions.DaysOfWeek.Count);
            int daysAdded = 0;
            foreach (var entity in dataOptions.DaysOfWeek)
            {
                if (!await dbContext.DaysOfWeek.AnyAsync(d => d.IsoNumber == entity.IsoNumber, cancellationToken))
                {
                    dbContext.DaysOfWeek.Add(new DayOfWeekEntity
                    {
                        Name = entity.Name,
                        ShortName = entity.ShortName,
                        IsoNumber = entity.IsoNumber,
                        IsWeekday = entity.IsWeekday,
                        SortOrder = entity.SortOrder
                    });
                    daysAdded++;
                    _logger.LogDebug("Added Day Of Week: {Name} (IsoNumber: {IsoNumber}).", entity.Name, entity.IsoNumber);
                }
            }
            _logger.LogInformation("Seeded {AddedCount} missing Days Of Week out of {TotalCount} requested for seed.", daysAdded, dataOptions.DaysOfWeek.Count);
        }

        if (dataOptions.SpecialCategories.Count > 0)
        {
            _logger.LogDebug("{Count} Special Categories found in default data settings.", dataOptions.SpecialCategories.Count);
            int specialAdded = 0;
            foreach (var entity in dataOptions.SpecialCategories)
            {
                if (!await dbContext.SpecialCategories.AnyAsync(s => s.Name == entity.Name, cancellationToken))
                {
                    dbContext.SpecialCategories.Add(new SpecialCategoryEntity
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Icon = entity.Icon,
                        SortOrder = entity.SortOrder
                    });
                    specialAdded++;
                    _logger.LogDebug("Added Special Category: {Name}.", entity.Name);
                }
            }
            _logger.LogInformation("Seeded {AddedCount} missing Special Categories out of {TotalCount} requested for seed.", specialAdded, dataOptions.SpecialCategories.Count);
        }

        if (dataOptions.VenueCategories.Count > 0)
        {
            _logger.LogDebug("{Count} Venue Categories found in default data settings.", dataOptions.VenueCategories.Count);
            int venueCatAdded = 0;
            foreach (var entity in dataOptions.VenueCategories)
            {
                if (!await dbContext.VenueCategories.AnyAsync(v => v.Name == entity.Name, cancellationToken))
                {
                    dbContext.VenueCategories.Add(new VenueCategoryEntity
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Icon = entity.Icon,
                        SortOrder = entity.SortOrder
                    });
                    venueCatAdded++;
                    _logger.LogDebug("Added Venue Category: {Name}.", entity.Name);
                }
            }
            _logger.LogInformation("Seeded {AddedCount} missing Venue Categories out of {TotalCount} requested for seed.", venueCatAdded, dataOptions.VenueCategories.Count);
        }

        if (dataOptions.VenueRoles.Count > 0)
        {
            _logger.LogDebug("{Count} Venue Roles found in default data settings.", dataOptions.VenueRoles.Count);
            int rolesAdded = 0;
            foreach (var entity in dataOptions.VenueRoles)
            {
                if (!await dbContext.VenueRoles.AnyAsync(r => r.Name == entity.Name, cancellationToken))
                {
                    dbContext.VenueRoles.Add(new VenueRoleEntity
                    {
                        Name = entity.Name,
                        Description = entity.Description
                    });
                    rolesAdded++;
                    _logger.LogDebug("Added Venue Role: {Name}.", entity.Name);
                }
            }
            _logger.LogInformation("Seeded {AddedCount} missing Venue Roles out of {TotalCount} requested for seed.", rolesAdded, dataOptions.VenueRoles.Count);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ensuring database exists.");
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                _logger.LogInformation("Database does not exist. Creating database.");
                await dbCreator.CreateAsync(cancellationToken);
                _logger.LogInformation("Database created successfully.");
            }
            else
            {
                _logger.LogInformation("Database already exists.");
            }
        });
    }

    private async Task RunMigrationAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Applying database migrations.");
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Database migrations applied successfully.");
        });
    }
}
