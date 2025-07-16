using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class BusinessHoursRepository : RepositoryBase<BusinessHoursEntity, long>, IBusinessHoursRepository
{
    public BusinessHoursRepository(ApplicationDbContext context) : base(context) { }
}
