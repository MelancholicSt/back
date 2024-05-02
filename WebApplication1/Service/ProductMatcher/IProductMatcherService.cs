using WebApplication1.Data.dao;

namespace WebApplication1.Service;

public interface IProductMatcherService
{
    long MatchByPrice(ClientOrder order, Offer offer);
    long MatchByDeliverTime(ClientOrder order, Offer offer);
    long MatchByRating(ClientOrder order, Offer offer);
}