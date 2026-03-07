using Application.DTOs;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<Result<UserResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result<UserResponse>> ValidateCredentialsAsync(LoginRequest request, CancellationToken cancellationToken = default);
}