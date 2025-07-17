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

    public async Task<UserEntity?> GetBySubAsync(string sub, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Sub == sub, cancellationToken);
    }

    public async Task AddAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public Task UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}