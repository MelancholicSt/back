using WebApplication1.Data.dao.Clients;

namespace WebApplication1.Data.dao.Clients;

public class Bucket
{
    public Client Owner { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
    public List<Products.Product> Products { get; set; } = new();
}