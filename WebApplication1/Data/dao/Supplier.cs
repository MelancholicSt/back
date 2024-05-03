namespace WebApplication1.Data.dao;

public class Supplier : Account
{
    public List<Order> PerformingOrders { get; set; } = new();
    public List<Material> AvailableMaterials { get; set; } = new();
    
}