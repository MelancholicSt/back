namespace WebApplication1.Data.dao.Product.Chars;

public class CharAttributeName
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public List<CharAttributeValue>? Values { get; } = new();
}