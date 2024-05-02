using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao;

[Table("product")]
public class Product
{
    [Key]
    public long ProductId { get; set; }
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public long PricePerQuantity { get; set; }
    
    public Measure Measure { get; set; }
}