using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.dao.Client;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]
public class ClientController : ControllerBase
{
    private UserManager<Client> _userManager;
    private INotificationSender _notificationSender;
    private DbContext _context;

    public ClientController(UserManager<Client> userManager, DbContext context,
        INotificationSender notificationSender)
    {
        _userManager = userManager;
        _context = context;
        _notificationSender = notificationSender;
    }
}