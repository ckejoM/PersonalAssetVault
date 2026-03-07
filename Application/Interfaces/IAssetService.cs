using Application.DTOs;

namespace Application.Interfaces;
public interface IAssetService
{
    Task<Result<AssetResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<AssetResponse>>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<AssetResponse>> CreateAsync(Guid userId, CreateAssetRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, Guid userId, UpdateAssetRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}