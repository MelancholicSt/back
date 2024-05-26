using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data.dao.Identity;

public class Account : IdentityUser
{
    
    public Image? ProfileImage { get; set; }

    public List<Invitation>? Invitations { get; } = new();

    public AccountInfo? AccountInfo { get; set; } = new();
    public Organization? Organization { get; set; } = null!;
    public ulong OrganizationId { get; set; }
}