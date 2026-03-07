using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using Mapster;

namespace Application.Services;
internal sealed class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AssetService(
        IAssetRepository assetRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _assetRepository = assetRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AssetResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var asset = await _assetRepository.GetByIdAsync(id, cancellationToken);

        if (asset is null)
        {
            return Result<AssetResponse>.Failure(new Error("Asset.NotFound", $"The asset with Id {id} was not found."));
        }

        return Result<AssetResponse>.Success(asset.Adapt<AssetResponse>());
    }

    public async Task<Result<IEnumerable<AssetResponse>>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var assets = await _assetRepository.GetByUserIdAsync(userId, cancellationToken);

        return Result<IEnumerable<AssetResponse>>.Success(assets.Adapt<IEnumerable<AssetResponse>>());
    }

    public async Task<Result<AssetResponse>> CreateAsync(Guid userId, CreateAssetRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Validate external constraints (e.g., does the category exist?)
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result<AssetResponse>.Failure(new Error("Category.NotFound", "The specified category does not exist."));
        }

        // 2. Delegate creation to the Domain Entity (Protecting invariants)
        var assetResult = Asset.Create(request.Name, request.Value, request.CategoryId, userId, request.AcquiredAt);

        if (assetResult.IsFailure)
        {
            return Result<AssetResponse>.Failure(assetResult.Error);
        }

        // 3. Orchestrate the persistence
        _assetRepository.Add(assetResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4. Return the mapped DTO
        return Result<AssetResponse>.Success(assetResult.Value.Adapt<AssetResponse>());
    }

    public async Task<Result> UpdateAsync(Guid id, Guid userId, UpdateAssetRequest request, CancellationToken cancellationToken = default)
    {
        var asset = await _assetRepository.GetByIdAsync(id, cancellationToken);

        if (asset is null || asset.UserId != userId)
        {
            return Result.Failure(new Error("Asset.NotFound", "The asset was not found or you do not have permission to modify it."));
        }

        var updateResult = asset.UpdateValue(request.Value);

        if (updateResult.IsFailure)
        {
            return Result.Failure(updateResult.Error);
        }

        _assetRepository.Update(asset);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var asset = await _assetRepository.GetByIdAsync(id, cancellationToken);

        if (asset is null || asset.UserId != userId)
        {
            return Result.Failure(new Error("Asset.NotFound", "The asset was not found or you do not have permission to delete it."));
        }

        _assetRepository.Delete(asset);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}