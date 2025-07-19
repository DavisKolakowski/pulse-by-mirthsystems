using System.Threading;
using System.Threading.Tasks;

using Application.Contracts.Repositories;
using Application.Domain.Entities;
using Application.Infrastructure.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity?> GetUserByNameIdentifierAsync(string nameIdentifier, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.NameIdentifier == nameIdentifier, cancellationToken);
    }
}