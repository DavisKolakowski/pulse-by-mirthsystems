using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class SpecialMenuScheduleRepository : RepositoryBase<SpecialMenuScheduleEntity, long>, ISpecialMenuScheduleRepository
{
    public SpecialMenuScheduleRepository(ApplicationDbContext context) : base(context) { }
}