using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class AssetRepository : IAssetRepository
{
    private readonly AssetVaultDbContext _context;
    public AssetRepository(AssetVaultDbContext context)
    {
        _context = context;
    }
    public void Add(Asset asset) => _context.Assets.Add(asset);
    public void Delete(Asset asset) => _context.Assets.Remove(asset);
    public Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _context.Assets.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    public async Task<IEnumerable<Asset>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await _context.Assets.Where(a => a.UserId == userId).ToListAsync(cancellationToken);
    public void Update(Asset asset) => _context.Assets.Update(asset);
}