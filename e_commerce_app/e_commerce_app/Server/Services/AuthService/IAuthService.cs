using e_commerce_app.Shared;

namespace e_commerce_app.Server.Services;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<bool> UserExist(string email);
    Task<ServiceResponse<String>> Login(string email, string password);
}