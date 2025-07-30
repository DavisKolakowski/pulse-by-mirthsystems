using System.Text.Json;
using System.Text.Json.Serialization;

using Application.API.Authorization.Handlers;
using Application.Contracts.Services;
using Application.Infrastructure.Data;
using Application.Infrastructure.Services;

using Hangfire;
using Hangfire.PostgreSql;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace Application.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.AddAzureNpgsqlDbContext<ApplicationDbContext>("application-db", configureDbContextOptions: options =>
        {
            options.UseNpgsql(npgsqlOptions =>
            {
                npgsqlOptions.UseNodaTime();
                npgsqlOptions.UseNetTopologySuite();
            })
            .UseSnakeCaseNamingConvention();

            #if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
            #endif
        });

        builder.Services.AddHangfire(config =>
        {
            config.UsePostgreSqlStorage(c =>
            {
                c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("application-db"));
            });
        });

        builder.Services.AddScoped<ISpecialMenuScheduler, SpecialMenuScheduler>();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddAuthentication()
                        .AddKeycloakJwtBearer(
                            serviceName: "keycloak",
                            realm: "application",
                            configureOptions: options =>
                            {
                                options.Audience = "application";

                                // For development only - disable HTTPS metadata validation
                                // In production, use explicit Authority configuration instead
                                if (builder.Environment.IsDevelopment())
                                {
                                    options.RequireHttpsMetadata = false;
                                }
                            });

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        builder.Services.AddSingleton<IAuthorizationHandler, VenueAuthorizationHandler>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseHangfireDashboard();

        app.Run();
    }
}
