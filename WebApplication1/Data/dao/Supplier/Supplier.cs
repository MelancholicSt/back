using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.dao.Product.Details;

namespace WebApplication1.Data.dao.Supplier;

public class Supplier : Account
{
    
    public float Rating { get; set; }
    
    public List<Order.Order>? PerformingOrders { get; set; } = new();
    public List<Product.Product>? AvailableProducts { get; } = new();
    public List<Material> AvailableMaterials { get; } = new();
}