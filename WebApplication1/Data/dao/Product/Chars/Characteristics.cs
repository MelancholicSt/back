namespace WebApplication1.Data.dao.Product;

public class Characteristics
{
    public ulong Id { get; set; }
    public CharKey Key { get; set; } = null!;
    public CharValue Value { get; set; } = null!;
    public ProductInfo ProductInfo { get; set; } = null!;
    public ulong ProductInfoId { get; set; }
}