using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_app.Shared;

public class Order
{
    public int id { get; set; }
    public int userId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    [Column(TypeName = "decimal()18,2")]
    public decimal TotalPrice { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
}