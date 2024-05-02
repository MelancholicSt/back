using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.dao.Clients;
using WebApplication1.Data.dao.Other;
using WebApplication1.Data.dao.Products;
using WebApplication1.Data.Identity;

namespace WebApplication1.Data.dao.Suppliers;

[Table("Suppliers")]
public class Supplier : Account
{
    public List<Order> PerformingOrders { get; set; } = new();
    public List<OfferedProduct> OfferingProducts { get; set; } = new();
    public List<Material> OfferingMaterials { get; set; } = new ();
}