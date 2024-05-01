using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]

public class NotificationController : ControllerBase
{
    private INotificationService _notificationService;
    private Logger<NotificationController> _notificationLogger;

    public NotificationController(INotificationService notificationService,
        Logger<NotificationController> notificationLogger)
    {
        _notificationService = notificationService;
        _notificationLogger = notificationLogger;
    }

    [HttpPost("notify/user/{userId}")]
    public async Task<IActionResult> SendNotification(string userId, [FromBody] string message)
    {
        await _notificationService.NotifyUser(userId, message);
        return Ok();
    }

    [HttpPost("notify/user/{userId}/delay={seconds}")]
    public async Task<IActionResult> SendDelayedNotification(string userId, int seconds, [FromBody] string message)
    {
        await Task.Run(async () =>
        {
            await Task.Delay(seconds / 1000);
            await _notificationService.NotifyUser(userId, message);
        });
        return Ok();
    }

    [HttpPost("notify/managers")]
    public async Task<IActionResult> SendToManagers([FromBody] string message)
    {
        await _notificationService.NotifyAllManagersAsync(message);
        _notificationLogger.Log(LogLevel.Information,
            $"{HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)} notified all managers with message:\n\t{message} ");
        return Ok();
    }
    
}