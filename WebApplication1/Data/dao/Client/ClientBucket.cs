namespace WebApplication1.Data.dao.Client;
using Product;
public class ClientBucket
{
    public ulong Id { get; set; }
    /// <summary>
    /// Products in bucket
    /// </summary>
    public List<Product>? Products { get; set; }
    /// <summary>
    /// Owner of this bucket
    /// </summary>
    public Client Client { get; set; } = null!;
    public string ClientId { get; set; } = null!;
}