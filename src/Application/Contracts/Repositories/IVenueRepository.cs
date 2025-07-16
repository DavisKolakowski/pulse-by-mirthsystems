using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IVenueRepository : IRepository<VenueEntity, long>
{
}