namespace WebApplication1.Data.dao;

public class Client : Account
{
    public FavouritesBucket FavouritesBucket { get; set; }
    public ClientBucket ClientBucket { get; set; }
    public List<Order> PlacedOrders { get; set; } = new();
}