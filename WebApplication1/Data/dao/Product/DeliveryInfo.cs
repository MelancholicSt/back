namespace WebApplication1.Data.dao.Product;

public class DeliveryInfo
{
    public ulong Id { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public float PricePerKm { get; set; }
}