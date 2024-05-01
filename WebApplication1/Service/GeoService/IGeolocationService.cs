using System.Net;

namespace WebApplication1;

public interface IGeolocationService
{
    Task<long> GetDistanceBetweenEndpointsAsync(IPAddress from, IPAddress to);
    public IPAddress GetIp(HttpContext ctx);
    double DegreesToRadians(double value);
}