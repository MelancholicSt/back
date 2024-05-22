using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private INotificationSender _notificationSender;
    private Logger<NotificationController> _notificationLogger;

    public NotificationController(INotificationSender notificationSender,
        Logger<NotificationController> notificationLogger)
    {
        _notificationSender = notificationSender;
        _notificationLogger = notificationLogger;
    }

    [HttpPost("notify/user/{userId}")]
    public async Task<IActionResult> SendNotification(string userId, [FromBody] string message)
    {
        await _notificationSender.NotifyUser(userId, message);
        return Ok();
    }

    [HttpPost("notify/user/{userId}/delay={seconds}")]
    public async Task<IActionResult> SendDelayedNotification(string userId, int seconds, [FromBody] string message)
    {
        await Task.Run(async () =>
        {
            await Task.Delay(seconds / 1000);
            await _notificationSender.NotifyUser(userId, message);
        });
        return Ok();
    }

    [HttpPost("notify/managers")]
    public async Task<IActionResult> SendToManagers([FromBody] string message)
    {
        await _notificationSender.NotifyAllManagersAsync(message);
        _notificationLogger.Log(LogLevel.Information,
            $"{HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)} notified all managers with message:\n\t{message} ");
        return Ok();
    }

    [HttpPost("notify/suppliers")]
    public async Task<IActionResult> SendToSuppliers([FromBody] string message)
    {
        await _notificationSender.NotifyAllSuppliers(message);
        return Ok();
    }
}