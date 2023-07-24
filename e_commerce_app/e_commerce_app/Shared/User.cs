namespace e_commerce_app.Shared;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}