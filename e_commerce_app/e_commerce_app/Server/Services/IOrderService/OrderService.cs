using System.Security.Claims;
using e_commerce_app.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Server.Services.IOrderService;

public class OrderService:IOrderService
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public OrderService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _dataContext = dataContext;
    }
    public async Task<ServiceResponse<bool>> PlaceOrder(Order Order)
    {
        Console.WriteLine("PlaceOrder");
        Order.userId = GetUserId();
        _dataContext.Orders.Add(Order);
         var response = await _dataContext.SaveChangesAsync();
         if (response > 0)
         {
             Console.WriteLine(response);
             return new ServiceResponse<bool>()
             {
                 Data = true,
                 Success = true,
                 Message = "Orders Placed Successfully"
             };
         }
         else
         {
             return new ServiceResponse<bool>()
             {
                 Data = false,
                 Success = false,
                 Message = "Problems Occured.Placing Order Failed"
             };
         }
    }

    [Authorize]
    public async Task<ServiceResponse<List<Order>>> GetOrders()
    {
        var userId = GetUserId();
        var orders = await _dataContext.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductType)
                    .Where(o => o.userId == userId)
                    .ToListAsync();

        if (orders != null)
        {
            return new ServiceResponse<List<Order>>()
            {
                Data = orders,
                Success = true,
                Message = "Orders by user extracted successfully"
            };
        }
        else
        {
            return new ServiceResponse<List<Order>>()
            {
                Data = null,
                Success = false,
                Message = "Problem Occured"
            };
        }
        
        

    }
    
    [Authorize]
    public async Task<ServiceResponse<Order>> GetOrder(int orderId)
    {
        var userId = GetUserId();
        var orders = await _dataContext.Orders
                                            .Include(o => o.OrderItems)
                                            .ThenInclude(oi => oi.ProductId)
                                            .Include(o => o.OrderItems)
                                            .ThenInclude(oi => oi.ProductType)
                                            .Where(o => o.userId == userId && o.id == orderId)
                                            .FirstOrDefaultAsync();

        if (orders != null)
        {
            return new ServiceResponse<Order>()
            {
                Data = orders,
                Success = true,
                Message = "Order by user extracted successfully"
            };
        }
        else
        {
            return new ServiceResponse<Order>()
            {
                Data = null,
                Success = false,
                Message = "Problem Occured"
            };
        }
    }

    public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
}