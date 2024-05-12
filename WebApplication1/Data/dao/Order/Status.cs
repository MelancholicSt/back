namespace WebApplication1.Data.dao.Order;


public class Status
{

    public uint Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Order>? Orders { get; set; }
    
}