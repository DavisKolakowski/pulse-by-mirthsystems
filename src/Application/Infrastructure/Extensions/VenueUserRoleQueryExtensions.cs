using Application.Domain.Entities;
using Application.Domain.Queries;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Extensions;

public static class VenueUserRoleQueryExtensions
{
    public static Task<VenueUserRoleEntity?> GetForUserAndVenueAsync(
        this IQueryable<VenueUserRoleEntity> query,
        Action<GetUserVenueRoleQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new GetUserVenueRoleQuery();
        configureQuery(searchQuery);

        return query
            .Include(vur => vur.Role)
            .Include(vur => vur.User)
            .FirstOrDefaultAsync(vur =>
                vur.UserId == searchQuery.UserId &&
                vur.VenueId == searchQuery.VenueId &&
                vur.IsActive,
                cancellationToken);
    }

    public static IQueryable<VenueUserRoleEntity> ByVenue(
        this IQueryable<VenueUserRoleEntity> query,
        Action<UserRolesByVenueQuery> configureQuery)
    {
        var searchQuery = new UserRolesByVenueQuery();
        configureQuery(searchQuery);

        query = query
            .Include(vur => vur.User)
            .Include(vur => vur.Role)
            .Include(vur => vur.GrantedByUser)
            .Where(vur => vur.VenueId == searchQuery.VenueId);

        if (searchQuery.ActiveOnly)
        {
            query = query.Where(vur => vur.IsActive);
        }

        return query.OrderBy(vur => vur.User.LastName).ThenBy(vur => vur.User.FirstName);
    }

    public static IQueryable<VenueUserRoleEntity> ByUser(
        this IQueryable<VenueUserRoleEntity> query,
        Action<VenueRolesByUserQuery> configureQuery)
    {
        var searchQuery = new VenueRolesByUserQuery();
        configureQuery(searchQuery);

        query = query
            .Include(vur => vur.Venue)
            .Include(vur => vur.Role)
            .Where(vur => vur.UserId == searchQuery.UserId);

        if (searchQuery.ActiveOnly)
        {
            query = query.Where(vur => vur.IsActive);
        }

        return query;
    }
}