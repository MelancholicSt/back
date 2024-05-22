using WebApplication1.Data.dao.Product;

namespace WebApplication1.Data.dao;

public class SupplierInfo
{
    public ulong Id { get; set; }
    public Supplier.Supplier Supplier { get; set; } = null!;
    public string SupplierId { get; set; }

}