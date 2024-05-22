namespace WebApplication1.Data.dao;

public class Category
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Product.Material>? Materials { get; } = new();
    public List<Category>? Children { get; } = new();
}