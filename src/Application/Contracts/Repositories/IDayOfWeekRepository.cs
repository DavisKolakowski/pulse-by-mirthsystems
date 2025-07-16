using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IDayOfWeekRepository : IRepository<DayOfWeekEntity, byte>
{
}