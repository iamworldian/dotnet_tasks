using e_commerce_app.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;

    public ProductService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _dataContext.Products
                                        .Include(p => p.Category)
                                        .Include(p => p.ProductVariants)
                                        .ThenInclude(p => p.ProductType)
                                        .ToListAsync()
        };
        return response;
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int id)
    {
        var response = new ServiceResponse<Product>();
        var product = await _dataContext.Products
                                            .Include(p => p.Category)
                                            .Include(p => p.ProductVariants)
                                            .ThenInclude(p => p.ProductType)
                                            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            response.Message = "The product not exist";
            response.Success = false;
        }
        else
        {
            response.Data = product;
        }

        return response;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(int id)
    {
        var response = await _dataContext.Products
                                                        .Where(p => p.CategoryId == id)
                                                        .Include(p => p.Category)
                                                        .Include(p => p.ProductVariants)
                                                        .ThenInclude(p => p.ProductType)
                                                        .ToListAsync();

        return new ServiceResponse<List<Product>>()
        {
            Data = response
        };
    }
    
    public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
    {
       
        var response = await FindProductFromSearchText(searchText);
        return new ServiceResponse<List<Product>>()
        {
            Data = response
        };
    }

    public Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
    {
        throw new NotImplementedException();
    }


    private async Task<List<Product>> FindProductFromSearchText(string searchText)
    {
        return await _dataContext.Products
            .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) || p.Description.ToLower().Contains(searchText.ToLower()))
            .Include(p => p.Category)
            .Include(p => p.ProductVariants)
            .ThenInclude(p => p.ProductType)
            .ToListAsync();

    }

    public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _dataContext.Products
                .Where(p => p.Featured == true)
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                .ThenInclude(p => p.ProductType)
                .ToListAsync()
        };

        return response;
    }
}