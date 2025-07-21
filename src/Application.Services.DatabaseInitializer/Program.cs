using Application.Data;
using Application.Extensions;
using Application.Services.DatabaseInitializer.Extensions;
using Application.Services.DatabaseInitializer.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.Services.DatabaseInitializer;

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
        builder.Services.AddProblemDetails();
        builder.Services.RegisterClock();
        builder.Services.ConfigureSerilog();
        builder.Services.ConfigureDataSetup(options =>
        {
            options.Bind(builder.Configuration.GetSection(DataOptions.ConfigurationSection));
        });
        builder.Services.AddTransient<DbSetup>();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}
