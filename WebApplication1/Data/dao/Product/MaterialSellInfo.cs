namespace WebApplication1.Data.dao.Product;

public class MaterialSellInfo
{
    public ulong Id { get; set; }
    public Material Material { get; set; } = null!;
    public ulong MaterialId { get; set; }
    
    public int PricePerMeasure { get; set; }
    
}