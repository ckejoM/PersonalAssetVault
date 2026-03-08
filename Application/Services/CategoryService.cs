using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetByUserIdAsync(userId, cancellationToken);

        return Result<IEnumerable<CategoryResponse>>.Success(categories.Adapt<IEnumerable<CategoryResponse>>());
    }
}