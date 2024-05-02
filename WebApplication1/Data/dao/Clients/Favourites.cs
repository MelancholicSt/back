namespace WebApplication1.Data.dao.Clients;

public class Favourites
{
    public string ClientId { get; set; } = null!;
    public Client Client { get; set; } = null!;
    public List<Products.Product> Products { get; set; } = new ();
}