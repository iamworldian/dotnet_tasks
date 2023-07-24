using System.Net.Http.Json;
using e_commerce_app.Shared;

namespace e_commerce_app.Client.Services.AuthService;

public class AuthService:IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResponse<int>> Register(UserRegistry userRegistry)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register", userRegistry);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login", userLogin);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }
}