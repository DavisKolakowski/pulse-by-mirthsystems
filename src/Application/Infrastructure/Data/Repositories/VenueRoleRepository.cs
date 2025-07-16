using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class VenueRoleRepository : RepositoryBase<VenueRoleEntity, int>, IVenueRoleRepository
{
    public VenueRoleRepository(ApplicationDbContext context) : base(context) { }
}