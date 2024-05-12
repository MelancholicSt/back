namespace WebApplication1.Data.dao.Product.Details;

public class Material
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public Measure Measure { get; set; } = new Measure
    {
        MeasureName = "Thing"
    };
}