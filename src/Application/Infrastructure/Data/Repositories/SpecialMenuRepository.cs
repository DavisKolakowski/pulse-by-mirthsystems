using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class SpecialMenuRepository : RepositoryBase<SpecialMenuEntity, long>, ISpecialMenuRepository
{
    public SpecialMenuRepository(ApplicationDbContext context) : base(context) { }
}