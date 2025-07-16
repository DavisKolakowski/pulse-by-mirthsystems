using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class VenueUserRoleRepository : RepositoryBase<VenueUserRoleEntity, long>, IVenueUserRoleRepository
{
    public VenueUserRoleRepository(ApplicationDbContext context) : base(context) { }
}