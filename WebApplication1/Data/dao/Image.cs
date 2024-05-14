using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.dao;

public class Image
{
    [Key]
    public string Guid { get; set; } = null!;

    public string Extension { get; set; } = null!;
}