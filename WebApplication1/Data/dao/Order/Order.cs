using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao.Order;
using Product;

public class Order
{
    [Key]
    public ulong OrderId { get; set; }
    
    public Status? Status { get; set; } = new ()
    {
        Name = "New"
    };
    public uint StatusId { get; set; }
    public List<Product>? Products { get; set; } = new ();
}