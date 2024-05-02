using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.Identity;

public class Account : IdentityUser
{
    public string FullName { get; set; }
    public string Info { get; set; }
    public float Rating { get; set; }
    public string ImagePath { get; set; }
}