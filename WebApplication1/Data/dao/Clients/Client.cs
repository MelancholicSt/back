using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Identity;

namespace WebApplication1.Data.dao.Clients;

[Table("Clients")]
public class Client : Account
{
    public List<Order> Orders { get; set; } = new();
    public Bucket Bucket { get; set; } = new ();
    public Favourites Favourites { get; set; } = new();
}