using WebApplication1.Data.dao.Identity;

namespace WebApplication1.Data.dao;

public class Offer
{
    public ulong Id { get; set; }
    
    public Account Sender { get; set; } = null!;
    public string SenderId { get; set; }
    
    public Account Receiver { get; set; } = null!;
    public string ReceiverId { get; set; }
}