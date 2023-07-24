using System.Text.Json;
using Blazored.LocalStorage;
using e_commerce_app.Shared;


namespace e_commerce_app.Client.Services.CartService;

public class CartService:ICartService
{
    private readonly ILocalStorageService _localStorageService;
    public CartService(ILocalStorageService localStorageService)
    {
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
}