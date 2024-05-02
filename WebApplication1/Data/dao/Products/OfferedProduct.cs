using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao.Products;

[Table("OfferedProducts")]
public class OfferedProduct : Product
{
    public int InStockAmount { get; set; }
    public int PricePerMeasure { get; set; }
}