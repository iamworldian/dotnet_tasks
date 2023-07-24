using e_commerce_app.Server.Services;
using Microsoft.AspNetCore.Mvc;
namespace e_commerce_app.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegistry userRegistry)
    {
        var response = await _authService.Register(new User() { Email = userRegistry.Email }, userRegistry.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin userlogin)
    {
        var response = await _authService.Login(userlogin.Email, userlogin.Password);

        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }



}