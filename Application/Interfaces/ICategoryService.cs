using Application.DTOs;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
}