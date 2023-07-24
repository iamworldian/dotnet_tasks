using e_commerce_app.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Server.Services.CategoryService;

public class CategoryService:ICategoryService
{
    private readonly DataContext _dataContext;

    public CategoryService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        var categories = await _dataContext.Categories.ToListAsync();

        return new ServiceResponse<List<Category>>()
        {
            Data = categories
        };
    }
}