using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Data.dao.Identity;

namespace WebApplication1.Data.dao.Client;
using Order;

/// <summary>
/// Entity which provides functionality of role "Client". Kind of extension of account entity
/// Might be that I'll change concept to other. 
/// </summary>
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
    public List<Order> Orders { get; } = new();
}