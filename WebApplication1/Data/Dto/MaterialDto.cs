namespace WebApplication1.Data.Dto;

public class MaterialDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Measure { get; set; }
    public string? MainImageGuid { get; set; }
    public ulong CategoryId { get; set; }
}