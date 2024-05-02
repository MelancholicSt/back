using Microsoft.AspNetCore.Identity;
using WebApplication1.Data.dao;

namespace WebApplication1;

public class Account : IdentityUser
{
    public Offer Offer { get; set; }
    
    public List<ClientOrder> Orders { get; set; } = new List<ClientOrder>();
}