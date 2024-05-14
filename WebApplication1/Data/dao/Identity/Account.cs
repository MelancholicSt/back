using Microsoft.AspNetCore.Identity;
using WebApplication1.Data.dao;

namespace WebApplication1;

public class Account : IdentityUser
{
    public AccountGeolocation? Geolocation { get; set; }
    public Organization Organization { get; set; } = null!;
    public ulong OrganizationId { get; set; }
}