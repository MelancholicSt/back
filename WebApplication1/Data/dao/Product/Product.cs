using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao;

[Table("product")]
public class Product
{
    [Key]
    public long Id { get; set; }
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;

    public Material Material { get; set; } = null!;
}