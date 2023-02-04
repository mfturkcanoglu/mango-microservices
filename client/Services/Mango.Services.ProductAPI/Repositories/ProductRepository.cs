using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<ProductDto> GetProducts()
    {
        var products= _dbContext.Products.AsEnumerable();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        return _mapper.Map<ProductDto>(await FindProductById(id));
    }

    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        
        // Id Generation starts from 1, and the mapper convert to product with Id 0
        if (product.ProductId == 0) 
        {
            await _dbContext.Products.AddAsync(product);
        }
        else
        {
            _dbContext.Products.Update(product);
        }
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        var product = await FindProductById(productId);
        if (product is null)
        {
            return false;
        }
        var result = _dbContext.Products.Remove(product); 
        await _dbContext.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }

    public async Task<Product?> FindProductById(int id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
    }
}