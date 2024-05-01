using System.Net;
using System.Security.Claims;
using Newtonsoft.Json;
using WebApplication1.Data.Dto;

namespace WebApplication1;

public class GeolocationService : IGeolocationService
{
    private IRequestService _requestService;

    public GeolocationService(IRequestService requestService)
    {
        _requestService = requestService;
 
    }
    
    public async Task<long> GetDistanceBetweenEndpointsAsync(IPAddress from, IPAddress to)
    {
 
        var fromGeo =
            JsonConvert.DeserializeObject<GeolocationDto>(
                await _requestService.GetJsonAsync($"http://ip-api.com/json/{from.MapToIPv4()}"));
        var toGeo = JsonConvert.DeserializeObject<GeolocationDto>(
            await _requestService.GetJsonAsync($"http://ip-api.com/json/{to.MapToIPv4()}"));
        double R = 6371f;
        var dLat = DegreesToRadians(toGeo.Lat - fromGeo.Lat);
        var dLon = DegreesToRadians(toGeo.Lon - fromGeo.Lon);
        var a =
            Math.Sin(dLat / 2) *
            Math.Sin(dLat / 2) +
            Math.Cos(DegreesToRadians(fromGeo.Lat)) * 
            Math.Cos(DegreesToRadians(toGeo.Lat)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var d = R * c;
        return Convert.ToInt64(d);
    }

    public IPAddress GetIp(HttpContext ctx)
    {
        var ipString = ctx.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (ipString != null)
            return IPAddress.Parse(ipString);
        return null;
    }

    public double DegreesToRadians(double value) => value * (Math.PI / 180);
}