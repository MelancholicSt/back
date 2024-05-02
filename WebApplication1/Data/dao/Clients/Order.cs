using WebApplication1.Data.dao.Other;
using WebApplication1.Data.dao.Suppliers;

namespace WebApplication1.Data.dao.Clients;


public class Order
{
    
    public long Id { get; set; }
    public long TotalPrice { get; set; } = 0;
    public string OwnerId { get; set; } = null!;
    public Client Owner { get; set; } = null!;
    
    public Supplier? Performer { get; set; }
    public string? PerformerId { get; set; }
    public List<Products.Product> Products { get; set; } = new();
    public Status Status { get; set; } = new()
    {
        Value = "New"
    };
}