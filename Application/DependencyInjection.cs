using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        MappingConfig.RegisterMappings();

        // Register the Application Services
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
