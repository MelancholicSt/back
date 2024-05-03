namespace WebApplication1.Data.dao;

public class FavouritesBucket
{
    public long Id { get; set; }
    public Client Client { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public List<Product> FavouriteProducts { get; set; } = new();
}