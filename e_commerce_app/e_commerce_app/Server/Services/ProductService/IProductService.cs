namespace e_commerce_app.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();
    Task<ServiceResponse<Product>> GetProductAsync(int id);
    Task<ServiceResponse<List<Product>>> GetProductsByCategory(int id);
    Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);
    
    Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
    
    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
}