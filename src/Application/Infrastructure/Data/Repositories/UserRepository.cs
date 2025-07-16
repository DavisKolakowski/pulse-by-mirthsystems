using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

namespace Application.Infrastructure.Data.Repositories;

public class UserRepository : RepositoryBase<UserEntity, long>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }
}