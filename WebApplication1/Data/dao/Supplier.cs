using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.dao.Product.Details;
using static WebApplication1.Data.dao.Product.Product;

namespace WebApplication1.Data.dao;

public class Supplier : Account
{
    
    public float Rating { get; set; }
    public List<Product.Product>? SellingProducts { get; set; } = new();
    public List<Order.Order>? Orders { get; set; } = new();
    public List<Material>? AvailableMaterials { get; set; } = new();
    
}