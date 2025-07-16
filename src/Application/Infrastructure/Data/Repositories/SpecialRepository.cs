using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class SpecialRepository : RepositoryBase<SpecialEntity, long>, ISpecialRepository
{
    public SpecialRepository(ApplicationDbContext context) : base(context) { }
}