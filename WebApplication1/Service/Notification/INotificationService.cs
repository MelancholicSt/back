namespace WebApplication1.Service;

public interface INotificationService
{
    Task NotifyAllAsync(string msg);
    Task NotifyUser(string userId, string msg);
    Task NotifyAllManagersAsync(string msg);
    Task NotifyAllSuppliers(string msg);
}