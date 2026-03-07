using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Shared;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
internal sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Check if email is already in use
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser is not null)
        {
            return Result<UserResponse>.Failure(new Error("Auth.EmailInUse", "This email is already registered."));
        }

        // 2. Hash the password securely
        var passwordHash = _passwordHasher.Hash(request.Password);

        // 3. Delegate to Domain Entity
        var userResult = User.Create(request.Email, passwordHash);
        if (userResult.IsFailure)
        {
            return Result<UserResponse>.Failure(userResult.Error);
        }

        // 4. Save to database
        _userRepository.Add(userResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UserResponse>.Success(userResult.Value.Adapt<UserResponse>());
    }

    public async Task<Result<UserResponse>> ValidateCredentialsAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            // Senior security practice: Keep login failure messages intentionally vague
            return Result<UserResponse>.Failure(new Error("Auth.InvalidCredentials", "Invalid email or password."));
        }

        return Result<UserResponse>.Success(user.Adapt<UserResponse>());
    }
}
