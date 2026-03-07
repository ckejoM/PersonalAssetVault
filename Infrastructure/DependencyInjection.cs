using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Data.Configurations;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Register DbContext
        services.AddDbContext<AssetVaultDbContext>(options =>
            options.UseSqlite(connectionString));

        // Register Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();

        // Register UnitOfWork (forwarding the interface to the DbContext)
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AssetVaultDbContext>());

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
