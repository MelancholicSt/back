using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Order;

namespace WebApplication1.Service;

public interface IProductMatchService
{
    long MatchByPrice(Order order);
    long MatchByDeliverTime(Order order);
    long MatchBySupplierRating(Order order);
}