namespace WebApplication1.Data.dao.Client;

public class OrderProduct
{
    public ulong Id { get; set; }
    public ClientBucket Bucket { get; set; } = null!;
    public ulong BucketId { get; set; }
    public ulong ProductId { get; set; }
    public ulong Quantity { get; set; }
}