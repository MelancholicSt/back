using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao.Order;
using Product;

public class Order
{
    [Key]
    public ulong Id { get; set; }

    public DateTime? ExpirationTime { get; set; }
    public Queue<Status> Statuses { get; } = new();

    public Client.Client Client { get; set; } = null!;
    public string ClientId { get; set; }
    
    public Supplier.Supplier? Supplier { get; set; }
    public string? SupplierId { get; set; }
    public List<Material>? Materials { get; } = new ();
}