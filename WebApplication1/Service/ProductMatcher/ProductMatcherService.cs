using WebApplication1.Data.dao;

namespace WebApplication1.Service;

public class ProductMatcherService : IProductMatcherService
{
    private DbContext _context;

    public ProductMatcherService(DbContext context)
    {
        _context = context;
    }

    public long MatchByPrice(Order order, Offer offer)
    {
        throw new NotImplementedException();
    }

    public long MatchByDeliverTime(Order order, Offer offer)
    {
        throw new NotImplementedException();
    }

    public long MatchByRating(Order order, Offer offer)
    {
        throw new NotImplementedException();
    }
}