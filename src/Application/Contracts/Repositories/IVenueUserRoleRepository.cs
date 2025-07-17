using System.Threading;
using System.Threading.Tasks;

using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IVenueUserRoleRepository
{
    Task<VenueUserRoleEntity?> GetUserRoleForVenueAsync(long userId, long venueId, CancellationToken cancellationToken = default);
}