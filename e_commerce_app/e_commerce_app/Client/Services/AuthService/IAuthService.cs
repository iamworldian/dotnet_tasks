using e_commerce_app.Shared;

namespace e_commerce_app.Client.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(UserRegistry userRegistry);
    Task<ServiceResponse<string>> Login(UserLogin userLogin);
}