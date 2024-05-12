namespace WebApplication1.Data.dao.Product;

public class CharValue
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public CharKey Key { get; set; } = null!;
    public ulong KeyId { get; set; }
}