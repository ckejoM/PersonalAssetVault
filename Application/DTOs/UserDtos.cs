namespace Application.DTOs;
public sealed record UserResponse(
    Guid Id,
    string Email,
    DateTime CreatedAtUtc);