using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;

namespace Mango.Services.ProductAPI.Repositories;

public interface IProductRepository
{
    IEnumerable<ProductDto> GetProducts();
    Task<ProductDto> GetProductById(int id);
    Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
    Task<bool> DeleteProduct(int productId);
    Task<Product?> FindProductById(int id);
}