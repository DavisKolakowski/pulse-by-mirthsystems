using Application.API.Authorization.Handlers;

using Microsoft.AspNetCore.Authorization;

namespace Application.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton<IAuthorizationHandler, VenueAuthorizationHandler>();
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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
