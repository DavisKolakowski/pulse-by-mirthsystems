using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ISpecialMenuRepository : IRepository<SpecialMenuEntity, long>
{
}