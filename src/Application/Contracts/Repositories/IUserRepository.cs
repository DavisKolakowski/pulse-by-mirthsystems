using System.Threading;
using System.Threading.Tasks;

using Application.Domain.Entities;

namespace Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetUserByNameIdentifierAsync(string nameIdentifier, CancellationToken cancellationToken = default);
}