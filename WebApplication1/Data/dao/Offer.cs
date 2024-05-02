using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.dao;

[Table("offer")]
public class Offer
{
    public long OfferId { get; set; }

    public Account Owner { get; set; } = null!;
    public string OwnerId { get; set; }
    
    public List<Product> Products { get; set; } = new ();
    public long DeliverPricePerKm { get; set; }
}