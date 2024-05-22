using WebApplication1.Data.dao.Identity;

namespace WebApplication1.Data.dao;

public class DeliverCompany
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Organization? CompanyOrganization { get; set; }
    
}