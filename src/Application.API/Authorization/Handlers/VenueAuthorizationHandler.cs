using System.Security.Claims;

using Application.Infrastructure.Data.Context;
using Application.Infrastructure.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.API.Authorization.Handlers;

public class VenueAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Guid>
{
    private readonly ApplicationDbContext _dbContext;

    public VenueAuthorizationHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Guid venueId)
    {
        var userSub = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userSub))
        {
            context.Fail();
            return;
        }

        if (context.User.HasClaim("roles", "system-administrator"))
        {
            context.Succeed(requirement);
            return;
        }

        if (context.User.HasClaim("roles", "content-manager"))
        {
            bool authorized = requirement == Operations.Venue.Access ||
                              requirement == Operations.Venue.Update ||
                              requirement == Operations.Venue.Activate ||
                              requirement == Operations.Venue.Deactivate ||
                              requirement == Operations.Venue.Specials.Create ||
                              requirement == Operations.Venue.Specials.Update ||
                              requirement == Operations.Venue.Specials.Activate ||
                              requirement == Operations.Venue.Specials.Deactivate ||
                              requirement == Operations.Venue.Specials.Delete;

            if (authorized)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return;
        }

        var user = await _dbContext.Users.GetByNameIdentifierAsync(q =>
        {
            q.NameIdentifier = userSub;
        });

        if (user == null)
        {
            context.Fail();
            return;
        }

        var userRole = await _dbContext.VenueUserRoles.GetForUserAndVenueAsync(q =>
        {
            q.UserId = user.Id;
            q.VenueId = venueId;
        });

        if (userRole == null || !userRole.IsActive)
        {
            context.Fail();
            return;
        }

        bool isAuthorized = false;
        switch (userRole.Role.Name)
        {
            case "venue-owner":
                isAuthorized = requirement == Operations.Venue.Access ||
                               requirement == Operations.Venue.Update ||
                               requirement == Operations.Venue.Activate ||
                               requirement == Operations.Venue.Deactivate ||
                               requirement == Operations.Venue.Users.Invite ||
                               requirement == Operations.Venue.Users.Uninvite ||
                               requirement == Operations.Venue.Users.Update ||
                               requirement == Operations.Venue.Users.Remove ||
                               requirement == Operations.Venue.Specials.Create ||
                               requirement == Operations.Venue.Specials.Update ||
                               requirement == Operations.Venue.Specials.Activate ||
                               requirement == Operations.Venue.Specials.Deactivate ||
                               requirement == Operations.Venue.Specials.Delete;
                break;
            case "venue-manager":
                isAuthorized = requirement == Operations.Venue.Access ||
                               requirement == Operations.Venue.Update ||
                               requirement == Operations.Venue.Specials.Create ||
                               requirement == Operations.Venue.Specials.Update ||
                               requirement == Operations.Venue.Specials.Activate ||
                               requirement == Operations.Venue.Specials.Deactivate ||
                               requirement == Operations.Venue.Specials.Delete;
                break;
            case "venue-staff":
                isAuthorized = requirement == Operations.Venue.Access ||
                               requirement == Operations.Venue.Specials.Create ||
                               requirement == Operations.Venue.Specials.Update ||
                               requirement == Operations.Venue.Specials.Activate ||
                               requirement == Operations.Venue.Specials.Deactivate ||
                               requirement == Operations.Venue.Specials.Delete;
                break;
            default:
                isAuthorized = false;
                break;
        }

        if (isAuthorized)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}