using WebApplication1.Data.dao.Identity;

namespace WebApplication1.Data.dao;

public class Invitation
{
    public ulong Id { get; set; }
    public Account Account { get; set; } = null!;
    public string AccountId { get; set; } = null!;
    public string Link { get; set; } = null!;
    public bool IsExpired { get; set; }
}