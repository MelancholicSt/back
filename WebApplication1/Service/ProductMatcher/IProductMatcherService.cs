using WebApplication1.Data.dao;

namespace WebApplication1.Service;

public interface IProductMatcherService
{
    long MatchByPrice(Order order, Offer offer);
    long MatchByDeliverTime(Order order, Offer offer);
    long MatchByRating(Order order, Offer offer);
}