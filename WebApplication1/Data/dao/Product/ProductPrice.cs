namespace WebApplication1.Data.dao;

public class ProductPrice
{
    public long Id { get; set; }
    public Product Product { get; set; } = null!;
    public long ProductId { get; set; }
    public int PricePerMeasure { get; set; }
}