using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.dao.Client;
using WebApplication1.Data.dao.Product.Details;

namespace WebApplication1.Data.dao.Product;

public class Product
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public Category Category { get; set; } = null!;
    public ulong CategoryId { get; set; }
    
    public Image? MainImage { get; set; }
    public Supplier.Supplier Supplier { get; set; }
    public string SupplierId { get; set; }
    public ProductInfo? Info { get; set; }
    public DeliveryInfo? DeliveryInfo { get; set; }
    
    public List<ProductSellInfo>? ProductSellInfos { get; } = new();
    public List<Order.Order>? Orders { get; } = new();
    public List<FavouritesBucket>? FavouritesBuckets { get; } = new();
    public List<ClientBucket>? ClientBuckets { get; } = new();
}