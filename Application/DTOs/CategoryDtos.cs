namespace Application.DTOs;
public sealed record CategoryResponse(
    Guid Id,
    string Name);
public sealed record CreateCategoryRequest(
    string Name);