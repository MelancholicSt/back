using Microsoft.AspNetCore.Identity;
using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.dao.Product;

namespace WebApplication1.Data.dao.Supplier;

public class Supplier : Account
{
    public List<Material>? Materials { get; } = new();
    public List<Order.Order>? PerformingOrders { get;} = new();
}