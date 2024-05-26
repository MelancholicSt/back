namespace WebApplication1.Data.dao.Client;

using Product;

public class ClientBucket
{
    public ulong Id { get; set; }

    public Client Client { get; set; } = null!;
    public string? ClientId { get; set; }
    
    /// <summary>
    /// Products in bucket
    /// </summary>
    public List<Material>? Materials { get;} = new();
    
}