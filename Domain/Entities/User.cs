using Domain.Shared;

namespace Domain.Entities;

public sealed class User : Entity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private User(Guid id, string email, string passwordHash, DateTime createdAtUtc)
        : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<User> Create(string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<User>.Failure(new Error("User.EmptyEmail", "Email is required."));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Result<User>.Failure(new Error("User.EmptyPassword", "Password hash is required."));
        }

        return Result<User>.Success(new User(Guid.NewGuid(), email, passwordHash, DateTime.UtcNow));
    }
}