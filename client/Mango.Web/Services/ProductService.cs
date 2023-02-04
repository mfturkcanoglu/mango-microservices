using Mango.Web.Models;
using Mango.Web.Models.Dtos;
using Mango.Web.Services.IServices;
using static Mango.Web.SD;

namespace Mango.Web.Services;

public class ProductService : BaseService, IProductService
{
    private static readonly string ProductApiBaseEndpoint = ProductAPIBase + "/api/Product";
    
    public ProductService(IHttpClientFactory httpClient) : base(httpClient)
    {
    }

    public async Task<T> GetAllProducts<T>()
    {
        return await this.SendAsync<T>(
            new ApiRequest()
            {
                Type = ApiType.GET,
                Url = ProductApiBaseEndpoint,
                AccessToken = ""
            }
        );
    }

    public async Task<T> GetProductByIdAsync<T>(int id)
    {
        return await this.SendAsync<T>(
            new ApiRequest()
            {
                Type = ApiType.GET,
                Url = $"{ProductApiBaseEndpoint}/{id}",
                AccessToken = ""
            }
        );
    }

    public async Task<T> CreateProductAsync<T>(ProductDto productDto)
    {
        return await this.SendAsync<T>(
            new ApiRequest()
            {
                Type = ApiType.POST,
                Url = ProductApiBaseEndpoint,
                Data = productDto,
                AccessToken = ""
            }
        );
    }

    public async Task<T> DeleteProductAsync<T>(int id)
    {
        return await this.SendAsync<T>(
            new ApiRequest()
            {
                Type = ApiType.DELETE,
                Url = $"{ProductApiBaseEndpoint}/{id}",
                AccessToken = ""
            }
        );
    }

    public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
    {
        return await this.SendAsync<T>(
            new ApiRequest()
            {
                Type = ApiType.PUT,
                Url = ProductApiBaseEndpoint,
                Data = productDto,
                AccessToken = ""
            }
        );
    }
}