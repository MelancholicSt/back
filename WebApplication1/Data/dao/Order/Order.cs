using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.dao;


public class Order
{
    [Key]
    public long OrderId { get; set; }

    public long TotalPrice { get; set; } = 0;
    public string OwnerId { get; set; } = null!;
    public Client Owner { get; set; } = null!;
    
    public string? PerformerId { get; set; }
    public Supplier? Performer { get; set; }
    public Status Status { get; set; } = new ();
    public List<Product> Products { get; set; } = new ();
    
}