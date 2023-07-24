using e_commerce_app.Shared;

namespace e_commerce_app.Client.Services.ProductService;

public interface IProductService
{
    event Action ProductsChanged;
    List<Product> Products { get; set; }

    Task GetProducts(int? categoryId = null);
    Task<ServiceResponse<Product>> GetProduct(int productId);
    Task SearchProducts(string searchText);
}