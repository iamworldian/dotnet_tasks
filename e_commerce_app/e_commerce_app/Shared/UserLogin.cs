using System.ComponentModel.DataAnnotations;

namespace e_commerce_app.Shared;

public class UserLogin
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}