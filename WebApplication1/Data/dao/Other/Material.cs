namespace WebApplication1.Data.dao.Other;

public class Material
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public Measure? Measure { get; set; } = new()
    {
        Value = "Things"
    };
}