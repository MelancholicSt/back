using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.dao.Identity;

public class Account : IdentityUser
{
    public AccountGeolocation? Geolocation { get; set; }
    public Organization Organization { get; set; } = null!;
    public ulong OrganizationId { get; set; }
}