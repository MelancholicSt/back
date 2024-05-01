using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controller;

[ApiController]
public class DevController : ControllerBase
{
    private IGeolocationService _geolocationService;
    private IEmailSender _emailSender;
    
    [ActivatorUtilitiesConstructor]
    public DevController(IGeolocationService geolocationService, IEmailSender emailSender)
    {
        _geolocationService = geolocationService;
        _emailSender = emailSender;
    }
    [HttpGet("/dev/delPrice")]
    public async Task<IActionResult> GetPrice(long perKiloPrice)
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
        IPAddress host = ipHostInfo.AddressList[0];
        var distance = await _geolocationService.GetDistanceBetweenEndpointsAsync(host, _geolocationService.GetIp(HttpContext));
        return Ok(new {price= distance * perKiloPrice});
        return Ok();
    }

    [HttpGet("dev/roles")]
    public async Task<bool> GetRoles()
    {
        return User.IsInRole("user") ;
    }

    [HttpGet("/dev/mail")]
    public async Task<IActionResult> Send()
    {
        await _emailSender.SendEmailAsync("fredysimonov@gmail.com", "s", "huy");
        return Ok("");
    }
}