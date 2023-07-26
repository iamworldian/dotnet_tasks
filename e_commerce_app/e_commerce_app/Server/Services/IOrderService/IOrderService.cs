namespace e_commerce_app.Server.Services.IOrderService;

public interface IOrderService
{
    public Task<ServiceResponse<bool>> PlaceOrder(Order Order);
    Task<ServiceResponse<List<Order>>> GetOrders();
    Task<ServiceResponse<Order>> GetOrder(int orderId);
}