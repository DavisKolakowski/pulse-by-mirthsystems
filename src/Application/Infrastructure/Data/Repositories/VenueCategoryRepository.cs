using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class VenueCategoryRepository : RepositoryBase<VenueCategoryEntity, int>, IVenueCategoryRepository
{
    public VenueCategoryRepository(ApplicationDbContext context) : base(context) { }
}