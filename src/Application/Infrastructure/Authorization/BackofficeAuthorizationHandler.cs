using System.Security.Claims;
using System.Threading.Tasks;

using Application.Contracts.Repositories;
using Application.Domain.Entities;

using Azure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Infrastructure.Authorization;

public class BackofficeAuthorizationHandler : AuthorizationHandler<VenueOperationRequirement, VenueEntity>
{
    private readonly IVenueUserRoleRepository _venueUserRoleRepo;

    public BackofficeAuthorizationHandler(IVenueUserRoleRepository venueUserRoleRepo)
    {
        _venueUserRoleRepo = venueUserRoleRepo;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        VenueOperationRequirement requirement,
        VenueEntity resource)
    {
        var userSub = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userSub))
        {
            context.Fail();
            return;
        }

        if (context.User.HasClaim("permissions", "system:admin"))
        {
            context.Succeed(requirement);
            return;
        }

        if (context.User.HasClaim("permissions", "content:manager"))
        {
            context.Succeed(requirement);
            return;
        }

        if (!long.TryParse(userSub, out var userId))
        {
            context.Fail();
            return;
        }

        var userRole = await _venueUserRoleRepo.GetUserRoleForVenueAsync(userId, resource.Id);
        if (userRole == null || !userRole.IsActive)
        {
            context.Fail();
            return;
        }

        bool authorized = userRole.Role.Name switch
        {
            "venue-owner" => true,
            "venue-manager" => requirement.Name switch
            {
                nameof(Operations.Menus.Create) or nameof(Operations.Menus.Read) or nameof(Operations.Menus.Update) or
                nameof(Operations.Menus.Activate) or nameof(Operations.Menus.Deactivate) or nameof(Operations.Menus.Delete) or
                nameof(Operations.Venues.Read) or nameof(Operations.Venues.Update) => true,
                _ => false
            },
            "venue-staff" => requirement.Name switch
            {
                nameof(Operations.Menus.Create) or nameof(Operations.Menus.Read) or nameof(Operations.Menus.Update) or
                nameof(Operations.Menus.Activate) or nameof(Operations.Menus.Deactivate) or nameof(Operations.Menus.Delete) or
                nameof(Operations.Venues.Read) => true,
                _ => false
            },
            _ => false
        };

        if (authorized)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}