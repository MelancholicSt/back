namespace WebApplication1.Data.dao.Product.Chars;

public class CharAttributeValue
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null!;
    public CharAttributeName AttributeName { get; set; } = null!;
    public ulong AttributeNameId { get; set; }
}