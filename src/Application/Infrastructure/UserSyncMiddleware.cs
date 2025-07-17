using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.Domain.Entities;
using Application.Contracts.Repositories;
using NodaTime;

namespace Application.Infrastructure;

public class UserSyncMiddleware
{
    private readonly RequestDelegate _next;

    public UserSyncMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepo, IClock clock)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var sub = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(sub))
            {
                var user = await userRepo.GetBySubAsync(sub);
                var now = clock.GetCurrentInstant();

                if (user == null)
                {
                    // Create new user on first login
                    user = new UserEntity
                    {
                        Sub = sub,
                        Email = context.User.FindFirst(ClaimTypes.Email)?.Value ?? "unknown@example.com",
                        IsActive = true,
                        CreatedAt = now,
                        UpdatedAt = now,
                        LastLoginAt = now
                    };
                    await userRepo.AddAsync(user);
                }
                else
                {
                    // Update existing
                    user.LastLoginAt = now;
                    user.UpdatedAt = now;
                    await userRepo.UpdateAsync(user);
                }

                await userRepo.SaveChangesAsync();
            }
        }

        await _next(context);
    }
}