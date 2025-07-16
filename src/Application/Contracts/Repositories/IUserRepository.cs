using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IUserRepository : IRepository<UserEntity, long>
{
}