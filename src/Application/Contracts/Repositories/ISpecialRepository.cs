using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ISpecialRepository : IRepository<SpecialEntity, long>
{
}