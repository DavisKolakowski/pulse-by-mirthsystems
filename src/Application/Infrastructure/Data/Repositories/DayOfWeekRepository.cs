using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class DayOfWeekRepository : RepositoryBase<DayOfWeekEntity, byte>, IDayOfWeekRepository
{
    public DayOfWeekRepository(ApplicationDbContext context) : base(context) { }
}