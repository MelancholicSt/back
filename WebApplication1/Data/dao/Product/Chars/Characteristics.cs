﻿namespace WebApplication1.Data.dao.Product.Chars;

public class Characteristics
{
    public ulong Id { get; set; }
    public CharAttributeValue AttributeValue { get; set; } = null!;
    public CharAttributeName AttributeName { get; set; } = null!;
}