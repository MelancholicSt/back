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

    public Material? Material { get; set; }
    public ulong MaterialId { get; set; }
}