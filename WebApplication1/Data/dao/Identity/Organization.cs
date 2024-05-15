namespace WebApplication1.Data.dao.Identity;

public class Organization
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Account> Accounts { get; } = new();
    public int INN { get; set; }
}