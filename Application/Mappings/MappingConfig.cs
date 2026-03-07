using Application.DTOs;
using Domain.Entities;
using Mapster;

namespace Application.Mappings;
public static class MappingConfig
{
    public static void RegisterMappings()
    {
        // Explicitly telling Mapster how to map Entity -> Response
        TypeAdapterConfig<Asset, AssetResponse>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.AcquiredAt, src => src.AcquiredAt);

        TypeAdapterConfig<Category, CategoryResponse>.NewConfig();

        TypeAdapterConfig<User, UserResponse>.NewConfig();
    }
}
