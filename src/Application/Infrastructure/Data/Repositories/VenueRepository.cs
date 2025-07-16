using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class VenueRepository : RepositoryBase<VenueEntity, long>, IVenueRepository
{
    public VenueRepository(ApplicationDbContext context) : base(context) { }
}