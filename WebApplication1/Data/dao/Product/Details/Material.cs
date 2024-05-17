namespace WebApplication1.Data.dao.Product.Details;

public class Material
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Supplier.Supplier> Suppliers { get; set; } = null!;
    public string SupplierId { get; set; }
    public Measure Measure { get; set; } = new()
    {
        MeasureName = "Thing"
    };
}