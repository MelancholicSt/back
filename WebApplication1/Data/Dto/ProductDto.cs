namespace WebApplication1.Data.Dto;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public MaterialDto Material { get; set; } 
}