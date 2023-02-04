using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;

namespace Mango.Services.ProductAPI.Configs;

public static class MappingConfig
{
    private static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Product, ProductDto>();
            config.CreateMap<ProductDto, Product>();
        });
        return mappingConfig;
    }

    public static IServiceCollection AddMaps(this IServiceCollection services)
    {
        services.AddSingleton(RegisterMaps().CreateMapper());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}