using Domain.Shared;

namespace Domain.Entities;

public sealed class Asset : Entity
{
    public string Name { get; private set; }
    public decimal Value { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime AcquiredAt { get; private set; }

    private Asset(Guid id, string name, decimal value, Guid categoryId, Guid userId, DateTime acquiredAt)
        : base(id)
    {
        Name = name;
        Value = value;
        CategoryId = categoryId;
        UserId = userId;
        AcquiredAt = acquiredAt;
    }

    public static Result<Asset> Create(string name, decimal value, Guid categoryId, Guid userId, DateTime acquiredAt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Asset>.Failure(new Error("Asset.EmptyName", "Asset name is required."));
        }

        if (value < 0)
        {
            return Result<Asset>.Failure(new Error("Asset.NegativeValue", "Asset value cannot be negative."));
        }

        if (categoryId == Guid.Empty || userId == Guid.Empty)
        {
            return Result<Asset>.Failure(new Error("Asset.InvalidReferences", "Valid Category ID and User ID are required."));
        }

        return Result<Asset>.Success(new Asset(Guid.NewGuid(), name, value, categoryId, userId, acquiredAt));
    }

    // Method to demonstrate encapsulating behavior
    public Result UpdateValue(decimal newValue)
    {
        if (newValue < 0)
        {
            return Result.Failure(new Error("Asset.NegativeValue", "Asset value cannot be negative."));
        }

        Value = newValue;
        return Result.Success();
    }
}