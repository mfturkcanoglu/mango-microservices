using Mango.Services.ProductAPI.Repositories;

namespace Mango.Services.ProductAPI.Configs;

public static class DIConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}