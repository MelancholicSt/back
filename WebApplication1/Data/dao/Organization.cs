namespace WebApplication1.Data.dao;

public class Organization
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Account>? Accounts { get; set; } = new();
    public int INN { get; set; }
}