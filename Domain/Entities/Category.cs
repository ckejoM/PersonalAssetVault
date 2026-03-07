using Domain.Shared;

namespace Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; }
    public Guid UserId { get; private set; }

    private Category(Guid id, string name, Guid userId) : base(id)
    {
        Name = name;
        UserId = userId;
    }

    public static Result<Category> Create(string name, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Category>.Failure(new Error("Category.EmptyName", "Category name is required."));
        }

        if (userId == Guid.Empty)
        {
            return Result<Category>.Failure(new Error("Category.InvalidUser", "A valid User ID is required."));
        }

        return Result<Category>.Success(new Category(Guid.NewGuid(), name, userId));
    }
}
