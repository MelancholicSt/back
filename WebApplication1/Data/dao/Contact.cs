using WebApplication1.Data.dao.Identity;

namespace WebApplication1.Data.dao;

public class Contact
{
    public ulong Id { get; set; }
    public List<string> Emails { get; } = new(); 
    public string? Number { get; set; }
    public string? TelegramTag { get; set; }
    public string? WhatsAppNumber { get; set; }
    
    public DeliverCompany? DeliverCompany { get; set; }
    public uint? DeliverCompanyId { get; set; }
    
    public Account? Account { get; set; }
    public string? AccountId { get; set; } 
}