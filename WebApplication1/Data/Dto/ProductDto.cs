namespace WebApplication1.Data.Dto;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string MaterialName { get; set; } = null!;
    public string? MeasureName { get; set; }
}