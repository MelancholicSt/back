using WebApplication1.Data.dao.Product.Chars;
using WebApplication1.Data.dao.Product.Details;

namespace WebApplication1.Data.dao.Product;

public class ProductInfo
{
    public ulong Id { get; set; }
    
    public List<Image>? Images { get; } = new();
    public Material? Material { get; set; }
    public string Description { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ulong ProductId { get; set; }
}