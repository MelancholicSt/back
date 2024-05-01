namespace WebApplication1.Data.Dto;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public int PricePerQuantity { get; set; }
    public string CategoryName { get; set; } = null!;
    public string MeasureName { get; set; } = "thing";
}