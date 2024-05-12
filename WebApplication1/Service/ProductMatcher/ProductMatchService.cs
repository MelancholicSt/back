using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Client;
using WebApplication1.Data.dao.Order;

namespace WebApplication1.Service.ProductMatcher;

public class ProductMatchService : IProductMatchService
{
    private DbContext _context;

    public ProductMatchService(DbContext context)
    {
        _context = context;
    }

    public long MatchByPrice(Order order)
    {
        throw new NotImplementedException();
    }

    public long MatchByDeliverTime(Order order)
    {
        throw new NotImplementedException();
    }

    public long MatchBySupplierRating(Order order)
    {
        throw new NotImplementedException();
    }
}