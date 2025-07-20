using Application.Domain.Entities;
using Application.Domain.Queries;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Extensions;

public static class UserQueryExtensions
{
    public static Task<UserEntity?> GetByNameIdentifierAsync(
        this IQueryable<UserEntity> query,
        Action<GetUserByNameIdentifierQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new GetUserByNameIdentifierQuery();
        configureQuery(searchQuery);

        return query.FirstOrDefaultAsync(
            u => u.NameIdentifier == searchQuery.NameIdentifier,
            cancellationToken);
    }

    public static Task<UserEntity?> GetByEmailAsync(
        this IQueryable<UserEntity> query,
        Action<GetUserByEmailQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new GetUserByEmailQuery();
        configureQuery(searchQuery);

        return query.FirstOrDefaultAsync(
            u => u.EmailAddress == searchQuery.EmailAddress,
            cancellationToken);
    }

    public static Task<UserEntity?> GetWithVenueRolesAsync(
        this IQueryable<UserEntity> query,
        Action<GetUserWithVenueRolesQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new GetUserWithVenueRolesQuery();
        configureQuery(searchQuery);

        return query
            .Include(u => u.VenueRoles)
                .ThenInclude(vr => vr.Role)
            .Include(u => u.VenueRoles)
                .ThenInclude(vr => vr.Venue)
            .FirstOrDefaultAsync(u => u.Id == searchQuery.UserId, cancellationToken);
    }

    public static IQueryable<UserEntity> ByVenue(
        this IQueryable<UserEntity> query,
        Action<UsersByVenueQuery> configureQuery)
    {
        var searchQuery = new UsersByVenueQuery();
        configureQuery(searchQuery);

        query = query
            .Include(u => u.VenueRoles)
                .ThenInclude(vr => vr.Role)
            .Where(u => u.VenueRoles.Any(vr => vr.VenueId == searchQuery.VenueId));

        if (searchQuery.ActiveOnly)
        {
            query = query.Where(u => u.VenueRoles.Any(vr =>
                vr.VenueId == searchQuery.VenueId && vr.IsActive));
        }

        return query.OrderBy(u => u.LastName).ThenBy(u => u.FirstName);
    }
}