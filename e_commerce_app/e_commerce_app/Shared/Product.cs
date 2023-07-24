using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_app.Shared;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public bool Featured { get; set; } = false;
    public List<ProductVariant> ProductVariants { get; set; } = new();

    public Product Clone()
    {
        return (Product)this.MemberwiseClone();
    }
}