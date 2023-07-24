using e_commerce_app.Server.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Server.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    // GET
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }
}