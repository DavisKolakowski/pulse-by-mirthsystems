using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Infrastructure.Authorization;
using Application.Infrastructure.Data.Repositories;
using Application.Options;
using Application.Services;

using Azure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

using NodaTime;

using Serilog;
using Serilog.Events;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSerilog(this IServiceCollection services)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Is(LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{Exception}");

        Log.Logger = loggerConfiguration.CreateLogger();
        services.AddLogging(builder => builder.AddSerilog(dispose: true));
        return services;
    }

    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, Action<ApplicationOptions> configuration)
    {
        var config = new ApplicationOptions();
        configuration(config);

        // Register configuration
        services.AddSingleton(config);

        services.AddMemoryCache();

        if (string.IsNullOrWhiteSpace(config.AzureMaps.SubscriptionKey))
        {
            throw new ArgumentException("Azure Maps key must be provided in the configuration.", nameof(config.AzureMaps.SubscriptionKey));
        }

        var azureMapsKeyCredential = new AzureKeyCredential(config.AzureMaps.SubscriptionKey);
        if (azureMapsKeyCredential != null)
        {
            services.AddScoped<IAzureMapsService>(serviceProvider => new AzureMapsService(azureMapsKeyCredential));
        }

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        // Register repositories
        RegisterRepositories(services);


        services.AddSingleton<IAuthorizationHandler, BackofficeAuthorizationHandler>();

        // TODO: Register other infrastructure services here
        // services.AddScoped<IFileStorageService, LocalFileStorageService>();
        // services.AddScoped<INotificationService, SignalRNotificationService>();
        return services;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVenueUserRoleRepository, VenueUserRoleRepository>();
    }
}
