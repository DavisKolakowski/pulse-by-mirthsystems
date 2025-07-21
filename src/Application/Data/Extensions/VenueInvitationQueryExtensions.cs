using Application.Data.Queries;
using Application.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Data.Extensions;

public static class VenueInvitationQueryExtensions
{
    public static Task<VenueInvitationEntity?> GetActiveAsync(
        this IQueryable<VenueInvitationEntity> query,
        Action<GetActiveVenueInvitationQuery> configureQuery,
        CancellationToken cancellationToken = default)
    {
        var searchQuery = new GetActiveVenueInvitationQuery();
        configureQuery(searchQuery);

        return query
            .Include(i => i.Role)
            .Include(i => i.Venue)
            .FirstOrDefaultAsync(i =>
                i.EmailAddress == searchQuery.EmailAddress &&
                i.VenueId == searchQuery.VenueId &&
                i.IsActive &&
                !i.AcceptedAt.HasValue &&
                i.ExpiresAt > searchQuery.CurrentInstant,
                cancellationToken);
    }

    public static IQueryable<VenueInvitationEntity> ByVenue(
        this IQueryable<VenueInvitationEntity> query,
        Action<VenueInvitationsByVenueQuery> configureQuery)
    {
        var searchQuery = new VenueInvitationsByVenueQuery();
        configureQuery(searchQuery);

        query = query
            .Include(i => i.Role)
            .Include(i => i.InvitedByUser)
            .Include(i => i.AcceptedByUser)
            .Where(i => i.VenueId == searchQuery.VenueId);

        if (!searchQuery.IncludeExpired && searchQuery.CurrentInstant.HasValue)
        {
            query = query.Where(i => i.IsActive && i.ExpiresAt > searchQuery.CurrentInstant.Value);
        }

        return query.OrderByDescending(i => i.CreatedAt);
    }

    public static IQueryable<VenueInvitationEntity> PendingByEmail(
        this IQueryable<VenueInvitationEntity> query,
        Action<PendingInvitationsByEmailQuery> configureQuery)
    {
        var searchQuery = new PendingInvitationsByEmailQuery();
        configureQuery(searchQuery);

        return query
            .Include(i => i.Venue)
            .Include(i => i.Role)
            .Where(i =>
                i.EmailAddress == searchQuery.EmailAddress &&
                i.IsActive &&
                !i.AcceptedAt.HasValue &&
                i.ExpiresAt > searchQuery.CurrentInstant)
            .OrderByDescending(i => i.CreatedAt);
    }
}