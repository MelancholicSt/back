using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.dao.Products;

[PrimaryKey(nameof(Name))]
public class Category
{
    public string Name { get; set; } = null!;
    public string? ParentName { get; set; }
    public List<Product> Products { get; set; } = new();
}