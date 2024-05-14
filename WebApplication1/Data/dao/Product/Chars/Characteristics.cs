﻿namespace WebApplication1.Data.dao.Product.Chars;

public class Characteristics
{
    public ulong Id { get; set; }
    public CharKey Key { get; set; } = null!;
    public CharValue Value { get; set; } = null!;
}