using System.Net.Http.Json;
using e_commerce_app.Shared;

namespace e_commerce_app.Client.Services.AuthService;

public class AuthService:IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = http;
        _authStateProvider = authStateProvider;
    }

    public async Task<ServiceResponse<int>> Register(UserRegistry userRegistry)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/register/", userRegistry);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/login/", userLogin);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }

    public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword userChangePassword)
    {
        var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", userChangePassword.Password);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
    }
    
    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }
    
}