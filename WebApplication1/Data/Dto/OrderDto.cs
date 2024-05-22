namespace WebApplication1.Data.Dto;

public class OrderDto
{
    public string OwnerId { get; set; } = null!;
    public string PerformerId { get; set; } = null!;
    public string Status { get; set; } = null!;
    public List<long> ProductIds { get; set; } = new ();
}