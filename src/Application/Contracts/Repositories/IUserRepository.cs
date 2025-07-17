using System.Threading;
using System.Threading.Tasks;

using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetBySubAsync(string sub, CancellationToken cancellationToken = default);
    Task AddAsync(UserEntity user, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}