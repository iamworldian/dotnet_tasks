using e_commerce_app.Shared;

namespace e_commerce_app.Client.Services.CartService;

public interface ICartService
{
    List<CartItem> ProductsInCart { get; set; }
    event Action OnChange;
    Task AddToCart(CartItem cartItem);
    Task RemovedFromCart();
    public Task<ServiceResponse<bool>> PlaceOrder(Order order);
}