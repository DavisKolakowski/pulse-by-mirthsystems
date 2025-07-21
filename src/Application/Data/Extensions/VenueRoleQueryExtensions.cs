using Application.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Data.Extensions;

public static class VenueRoleQueryExtensions
{
    public static Task<VenueRoleEntity?> GetByNameAsync(
        this IQueryable<VenueRoleEntity> query,
        string roleName,
        CancellationToken cancellationToken = default)
    {
        return query.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
    }
}