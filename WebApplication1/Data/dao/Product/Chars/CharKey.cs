﻿namespace WebApplication1.Data.dao.Product;

public class CharKey
{
    public ulong Id { get; set; }
    public string Name { get; set; } = null;
    public List<CharValue> Values { get; set; } = new();
}