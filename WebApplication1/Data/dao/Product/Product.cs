using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.dao.Product.Details;

namespace WebApplication1.Data.dao.Product;

public class Product
{
    public ulong Id { get; set; }
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    
    private Image? ProductImage { get; set; }
    public ProductInfo? Info { get; set; }
}