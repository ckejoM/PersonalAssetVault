using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly AssetVaultDbContext _context;
    public UserRepository(AssetVaultDbContext context)
    {
        _context = context;
    }
    public void Add(User user) => _context.Users.Add(user);
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
}
