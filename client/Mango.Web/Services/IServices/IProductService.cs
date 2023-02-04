using Mango.Web.Models.Dtos;

namespace Mango.Web.Services.IServices;

public interface IProductService : IBaseService
{
    Task<T> GetAllProducts<T>();
    Task<T> GetProductByIdAsync<T>(int id);
    Task<T> CreateProductAsync<T>(ProductDto productDto);
    Task<T> DeleteProductAsync<T>(int id);
    Task<T> UpdateProductAsync<T>(ProductDto productDto);
}