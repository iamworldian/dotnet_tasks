using System.Net.Http.Json;
using e_commerce_app.Shared;

namespace e_commerce_app.Client.Services.ProductService;

public class ProductService: IProductService
{
    private readonly HttpClient _http;
    private IProductService _productServiceImplementation;

    public ProductService(HttpClient http)
    {
        _http = http;
    }
    public event Action? ProductsChanged;
    public List<Product> Products { get; set; } = null!;
    public string Message { get; set; } = String.Empty;

    public async Task GetProducts(int? categoryId = null)
    {
        var result = categoryId == null ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/"):
            await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryId}/");
        if (result != null && result.Data != null)
            Products = result.Data;
        ProductsChanged.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}/");
        return result;
    }

    public async Task SearchProducts(string searchText)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchText}/");

        if (result != null && result.Data != null)
        {
            Products = result.Data;
        }

        if (Products.Count == 0) Message = "No Products Found";
        ProductsChanged.Invoke();
    }
    }

    