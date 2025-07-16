using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class VenueInvitationRepository : RepositoryBase<VenueInvitationEntity, long>, IVenueInvitationRepository
{
    public VenueInvitationRepository(ApplicationDbContext context) : base(context) { }
}