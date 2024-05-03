using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.dao;


public class Status
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
}