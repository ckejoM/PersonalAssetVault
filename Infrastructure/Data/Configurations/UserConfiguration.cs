using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        // Ensure emails are unique at the database level
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();
    }
}

public sealed class AssetVaultDbContext : DbContext, IUnitOfWork
{
    public AssetVaultDbContext(DbContextOptions<AssetVaultDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Asset> Assets => Set<Asset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This line automatically finds all our IEntityTypeConfiguration classes 
        // in the Infrastructure assembly and applies them.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssetVaultDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    // IUnitOfWork implementation is implicitly satisfied because DbContext 
    // already has a SaveChangesAsync(CancellationToken) method.
}