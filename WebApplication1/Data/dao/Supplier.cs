namespace WebApplication1.Data.dao;

public class Supplier
{
    public string AccountId { get; set; } = null!;
    public List<Material> Materials { get; set; } = new ();
}