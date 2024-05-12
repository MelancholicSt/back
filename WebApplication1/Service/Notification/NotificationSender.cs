using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Service;

public class NotificationSender : INotificationSender
{
    private IEmailSender _emailSender;
    private UserManager<Account> _userManager;
    private ILogger _logger;

    public NotificationSender(IEmailSender emailSender, UserManager<Account> userManager,
        ILogger logger)
    {
        _emailSender = emailSender;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task NotifyAllAsync(string msg)
    {
        await _userManager.Users.ForEachAsync(
            async u => await _emailSender.SendEmailAsync(u.Email, "Notification", msg));
    }

    public async Task NotifyUser(string userId, string msg)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return;
        await _emailSender.SendEmailAsync(user.Email, "Notification", msg);
    }

    public async Task NotifyAllManagersAsync(string msg)
    {
        var managers = await _userManager.GetUsersInRoleAsync("Manager");
        await managers.AsQueryable().ForEachAsync(x => _emailSender.SendEmailAsync(x.Email, "Notification", msg));
    }

    public Task NotifyAllSuppliers(string msg)
    {
        throw new NotImplementedException();
    }
}