using Application.Infrastructure.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Application.Services.DatabaseMigrations;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddServiceDefaults();
        builder.AddAzureNpgsqlDbContext<ApplicationDbContext>("application-db", configureDbContextOptions: options =>
        {
            options.UseNpgsql(npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                npgsqlOptions.UseNodaTime();
                npgsqlOptions.UseNetTopologySuite();
            })
            .UseSnakeCaseNamingConvention();

            #if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
            #endif
        });
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}