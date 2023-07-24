using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using e_commerce_app.Server.Data;
using e_commerce_app.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace e_commerce_app.Server.Services;

public class AuthService:IAuthService
{
    private readonly DataContext _dataContext;
    private readonly IConfiguration _configuration;
    public AuthService(DataContext dataContext, IConfiguration configuration)
    {
        _dataContext = dataContext;
        _configuration = configuration;
    }
    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        if (await UserExist(user.Email))
        {
            return new ServiceResponse<int>()
            {
                Success = false,
                Message = "User Already Exist\n"
            };
        }
        
        CreateHashPassword(password , out byte[] passwordHash , out byte[] paswordSalt);
        user.Password = passwordHash;
        user.PasswordSalt = paswordSalt;

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return new ServiceResponse<int>()
        {
            Data = user.Id,
            Message = "User Registration Successful",
        };
    }

    public async Task<bool> UserExist(string email)
    {
        if (await _dataContext.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower())))
        {
            return true;
        }
        else return false;
    }

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var response = new ServiceResponse<string>();
        var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));

        if (user == null)
        {
            response.Message = "User doesn't Exists.U need to register first";
            response.Success = false;
        }else if (!VerifyPassword(password, user.Password, user.PasswordSalt))
        {
            response.Message = "Wrong Email/Password Combination";
            response.Success = false;
        }
        else
        {
            response.Data = CreateToken(user);
            response.Success = true;
            response.Message = "User Logged in Succesfully";
        }

        return response;
    }

    private void CreateHashPassword(string password, out byte[] passwrodHash, out byte[] paswordSalt)
    {
        using (var mac = new HMACSHA512())
        {
            paswordSalt = mac.Key;
            passwrodHash = mac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
            new Claim(ClaimTypes.Name , user.Email)
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims, 
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}   