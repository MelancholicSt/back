namespace WebApplication1.Data.dao;

public class ClientOffer
{
    public Account? Client { get; set; }
    public long OrderId { get; set; }
    public ClientOrder Order { get; set; }
    public Offer Offer { get; set; } = null!;
}