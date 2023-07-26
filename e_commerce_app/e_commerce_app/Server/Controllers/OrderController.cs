using e_commerce_app.Server.Services.IOrderService;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce_app.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class OrderController: Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> PostOrder(Order order)
    {
        var result = await _orderService.PlaceOrder(order);
        return Ok(result);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ServiceResponse<List<Order>>>> GetOrders()
    {
        Console.WriteLine("Order Controller");
        var result = await _orderService.GetOrders();
        if (result.Data != null) return Ok(result);
        else return BadRequest(result);
    }
}