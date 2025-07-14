
namespace Application.Services.Identity.API;

using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add service defaults & Aspire client integrations.
        builder.AddServiceDefaults();
        builder.AddAzureNpgsqlDbContext<ApplicationDbContext>("application-db", configureDbContextOptions: options =>
        {
            options.UseNpgsql(npgsqlOptions =>
            {
                npgsqlOptions.UseNodaTime();
                npgsqlOptions.UseNetTopologySuite();
            })
            .UseSnakeCaseNamingConvention();
        });

        // Add services to the container.
        builder.Services.AddProblemDetails();

        builder.Services.AddIdentityApiEndpoints<UserEntity>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGroup("/.identity")
            .MapIdentityApi<UserEntity>()
            .WithName("Identity API Endpoints");

        app.MapDefaultEndpoints();

        app.Run();
    }
}
