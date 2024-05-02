using WebApplication1.Data.dao.Clients;

namespace WebApplication1.Data.dao.Other;

public class Status
{
    public int StatusId { get; set; } 
    public string Value { get; set; } = null!;

    public Order Order { get; set; } = null!;
    public long OrderId { get; set; } 
}