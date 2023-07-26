using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
namespace e_commerce_app.Client;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;

    public CustomAuthStateProvider(ILocalStorageService localStorageService,HttpClient httpClient)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Get the token for localstorage saved a {key,pair}
        string authToken = await _localStorageService.GetItemAsStringAsync("authToke");
        // initializes a new ClaimsIdentity object with an empty set of claims collection
        var identity = new ClaimsIdentity();
        // settings default request Headers for authorization
        _httpClient.DefaultRequestHeaders.Authorization = null;
        if (!string.IsNullOrEmpty(authToken))
        {
            try
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
            }
            catch
            {
                await _localStorageService.RemoveItemAsync("authToke");
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "==";
                break;
            case 3: base64 += "=";
                break;
        }
        return Convert.FromBase64String(base64);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        Console.WriteLine(payload);
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        return claims;
    }
}