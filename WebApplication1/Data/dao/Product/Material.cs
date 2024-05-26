using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.dao.Client;

namespace WebApplication1.Data.dao.Product;

public class Material
{
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public Category Category { get; set; } = null!;
    public ulong CategoryId { get; set; }
    
    public string Description { get; set; } = null!;
    public string? Measure { get; set; } = "Thing";
    
    public Image? MainImage { get; set; }
    public ulong MainImageId { get; set; }
    public List<Image>? Images { get; } = new();
    public List<Order.Order>? Orders { get; } = new();
    public List<Supplier.Supplier> Suppliers { get; } = new();
    public List<FavouritesBucket>? FavouritesBuckets { get; } = new();
    public List<ClientBucket>? ClientBuckets { get; } = new();
}