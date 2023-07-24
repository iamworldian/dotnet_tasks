using e_commerce_app.Server.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Server.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{

    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
    {
        var response = await _productService.GetProductsAsync();
        return Ok(response);

    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int id)
    {
        var response = await _productService.GetProductAsync(id);
        return Ok(response);
    }

    [HttpGet("category/{id:int}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(int id)
    {
        var result = await _productService.GetProductsByCategory(id);
        return Ok(result);
    }
    
    [HttpGet("search/{searchText}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string searchText)
    {
        var result = await _productService.SearchProducts(searchText);
        return Ok(result);
    }
    
    
    [HttpGet("featured")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
    {
        var result = await _productService.GetFeaturedProducts();
        return Ok(result);
    }



}