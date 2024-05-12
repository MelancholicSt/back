using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao.Client;
using Order;


public class Client : Account
{
    /// <summary>
    /// Storage where contains client favourite products
    /// </summary>
    public FavouritesBucket? FavouritesBucket { get; set; }
    /// <summary>
    /// Storage which reperesents products for new order
    /// </summary>
    public ClientBucket? ClientBucket { get; set; } 
    /// <summary>
    /// All client orders. Not depended on state
    /// </summary>
    public List<Order>? Orders { get; set; }
}