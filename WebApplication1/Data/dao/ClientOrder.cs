using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.dao;

[Table("userorder")]
public class ClientOrder
{
    [Key]
    public long OrderId { get; set; }

    public long TotalPrice { get; set; } = 0;
    public string OwnerId { get; set; }
    public Status Status { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();
    
}