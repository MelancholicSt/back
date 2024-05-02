using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao;

[Table("status")]
public class Status
{
    [Key]
    public int StatusId { get; set; }
    public ClientOrder Order { get; set; }
    public long OrderId { get; set; }
    public string Name { get; set; } = null!;
    
}