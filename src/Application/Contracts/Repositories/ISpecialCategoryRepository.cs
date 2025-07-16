using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ISpecialCategoryRepository : IRepository<SpecialCategoryEntity, int>
{
}