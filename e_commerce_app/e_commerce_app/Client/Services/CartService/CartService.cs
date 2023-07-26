using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using e_commerce_app.Shared;


namespace e_commerce_app.Client.Services.CartService;

public class CartService:ICartService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    public CartService(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }
    public event Action OnChange;
 

    public List<CartItem> ProductsInCart { get; set; } = new List<CartItem>();

    public async Task AddToCart(CartItem cartItem)
    {
        ProductsInCart.Add(cartItem);
        OnChange.Invoke();
    }

    public async Task RemovedFromCart()
    {
        OnChange.Invoke();
    }

    public int CartCount()
    {
        return ProductsInCart.Count();
    }

    public async Task<ServiceResponse<bool>> PlaceOrder(Order order)
    {
        var response = await _httpClient.PostAsJsonAsync("api/order/", order);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }
}