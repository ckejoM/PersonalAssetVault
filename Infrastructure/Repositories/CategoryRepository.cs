using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly AssetVaultDbContext _context;
    public CategoryRepository(AssetVaultDbContext context)
    {
        _context = context;
    }
    public void Add(Category category) => _context.Categories.Add(category);
    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _context.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    public async Task<IEnumerable<Category>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await _context.Categories.Where(c => c.UserId == userId).ToListAsync(cancellationToken);
}