namespace WebApplication1.Data.dao;

public class Material
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public Measure Measure { get; set; } = new Measure
    {
        MeasureName = "Thing"
    };
}