using System.ComponentModel.DataAnnotations;

namespace e_commerce_app.Shared;

public class UserChangePassword
{
    [Required,StringLength(20,MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
    [Compare("Password" , ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}