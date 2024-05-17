using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.dao.Identity;

public class AccountInfo
{
    [Key]
    public ulong Id { get; set; }
    public Account Account { get; set; } = null!;
    public string AccountId { get; set; }
    
    public string Name { get; set; }
    public string Surname { get; set; }
    public AccountGeolocation? Geolocation { get; set; }
}