namespace WebApplication1.Data.dao;

public class ClientBucket
{
    public long Id { get; set; }
    public List<BucketProduct> Products { get; set; } = new ();
    public Client Client { get; set; } = null!;
    public string ClientId { get; set; } = null!;
}