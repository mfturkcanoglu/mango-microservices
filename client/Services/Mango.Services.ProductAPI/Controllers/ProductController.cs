using System.Linq.Expressions;
using Mango.Services.ProductAPI.Models.Dtos;
using Mango.Services.ProductAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private ResponseDto _responseDto { get; set; } = new();

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ResponseDto>> GetProduct(int id)
    {
        return await HandleRequest(async () => await _productRepository.GetProductById(id));
    }

    [HttpGet]
    public ActionResult<ResponseDto> GetProducts()
    {
        var products = _productRepository.GetProducts();
        _responseDto.Result = products;
        return Ok(_responseDto);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> Create([FromBody] ProductDto productDto)
    {
        return await CreateOrUpdateProduct(productDto);
    }
    
    [HttpPut]
    public async Task<ActionResult<ResponseDto>> Update([FromBody] ProductDto productDto)
    {
        return await CreateOrUpdateProduct(productDto);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<ResponseDto>> Delete(int id)
    {
        return await HandleRequest(async() => await _productRepository.DeleteProduct(id));
    }

    [NonAction]
    private async Task<ActionResult<ResponseDto>> CreateOrUpdateProduct([FromBody] ProductDto productDto)
    {
        return await HandleRequest(async () => await _productRepository.CreateUpdateProduct(productDto));
    }
    
    [NonAction]
    private async Task<ActionResult<ResponseDto>> HandleRequest(Func<Task<object>> crudAction)
    {
        try
        {
            _responseDto.Result = await crudAction();
        }
        catch (Exception e)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages.Add(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, _responseDto);
        }

        return Ok(_responseDto);
    }
}