using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data.Repositories;

public class VenueUserRoleRepository : IVenueUserRoleRepository
{
    private readonly ApplicationDbContext _context;
    public VenueUserRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<VenueUserRoleEntity?> GetVenueRoleForUserAsync(Guid userId, Guid venueId, CancellationToken cancellationToken = default)
    {
        return await _context.VenueUserRoles
            .Include(vur => vur.Role)
            .FirstOrDefaultAsync(vur => vur.UserId == userId && vur.VenueId == venueId, cancellationToken);
    }
}
