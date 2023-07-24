using System.Security.Cryptography.X509Certificates;

namespace e_commerce_app.Shared;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}