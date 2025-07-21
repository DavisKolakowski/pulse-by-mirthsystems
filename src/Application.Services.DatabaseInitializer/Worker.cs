using System.Diagnostics;

using Application.Data;
using Application.Services.DatabaseInitializer.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace Application.Services.DatabaseInitializer;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly ActivitySource _activitySource;

    public Worker(
        IServiceProvider serviceProvider,
        IHostEnvironment hostEnvironment,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger)
    {
        this._serviceProvider = serviceProvider;
        this._hostEnvironment = hostEnvironment;
        this._hostApplicationLifetime = hostApplicationLifetime;
        this._logger = logger;
        this._activitySource = new ActivitySource(_hostEnvironment.ApplicationName);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = this._activitySource.StartActivity(_hostEnvironment.ApplicationName, ActivityKind.Client);

        try
        {
            using var scope = this._serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var dbSetup = scope.ServiceProvider.GetRequiredService<DbSetup>();

            this._logger.LogInformation("Starting database initialization...");

            await dbSetup.RunAsync(dbContext, scope.ServiceProvider.GetRequiredService<IOptions<DataOptions>>(), cancellationToken);

            this._logger.LogInformation("Database initialization completed successfully.");
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, "Error during database initialization.");
            activity?.AddException(ex);
            throw;
        }

        this._hostApplicationLifetime.StopApplication();
    }
}
