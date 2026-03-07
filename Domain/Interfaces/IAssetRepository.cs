using Domain.Entities;

namespace Domain.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Asset>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    void Add(Asset asset);
    void Update(Asset asset);
    void Delete(Asset asset);
}