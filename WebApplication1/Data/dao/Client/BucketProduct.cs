namespace WebApplication1.Data.dao;

public class BucketProduct
{
    public long Id { get; set; }
    public Product Product { get; set; } = null!;
    public long ProductId { get; set; }
    public long Quantity { get; set; }
}