using System.Net.Http.Json;
using e_commerce_app.Shared;
using Microsoft.AspNetCore.Components;

namespace e_commerce_app.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;
        
        public OrderService(HttpClient http,
            AuthenticationStateProvider authStateProvider,
            NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<Order> GetOrderDetails(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Order>>($"api/order/{orderId}");
            return result.Data;
        }

        public async Task<List<Order>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Order>>>("api/order/");
            return result.Data;
        }

    }
}