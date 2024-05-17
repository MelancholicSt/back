namespace WebApplication1.Data.dao.Product;

public class ProductSellInfo
{
    public ulong Id { get; set; }
    public Product Product { get; set; } = null!;
    public ulong ProductId { get; set; }
    
    public int PricePerMeasure { get; set; }
    
}