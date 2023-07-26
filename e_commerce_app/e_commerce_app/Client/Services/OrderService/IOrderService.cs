using e_commerce_app.Shared;

namespace e_commerce_app.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderDetails(int orderId);
    }
}