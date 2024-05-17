using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.dao.Product;

namespace WebApplication1.Data.dao;

public class Image
{
    [Key] public string Guid { get; set; } = null!;

    public string Extension { get; set; } = null!;
    public string Name { get; set; } = null!;

    public Product.Product? Product { get; set; }
    public ulong ProductId { get; set; }

    public ProductInfo? ProductInfo { get; set; } 
    public ulong ProductInfoId { get; set; }
}