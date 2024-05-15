using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao.Client;
using WebApplication1.Data.dao.Order;
using WebApplication1.Data.dao.Product;
using WebApplication1.ProductShowcaseService;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]
public class ClientController : ControllerBase
{
    private UserManager<Client> _clientManager;
    private INotificationSender _notificationSender;
    private IProductShowcaseService _productShowcaseService;
    private DbContext _context;

    public ClientController(UserManager<Client> clientManager, DbContext context,
        INotificationSender notificationSender)
    {
        _clientManager = clientManager;
        _context = context;
        _notificationSender = notificationSender;
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        Client client = await GetClientInstanceAsync();
        return Ok(JsonConvert.SerializeObject(client.Orders));
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetOrder(ulong orderId)
    {
        Client client = await GetClientInstanceAsync();
        Order? order = client.Orders.FirstOrDefault(x => x.OrderId == orderId);
        if (order == null)
            return NotFound("Order not found");
        return Ok(JsonConvert.SerializeObject(order));
    }

    [HttpPost("order/create")]
    public async Task<IActionResult> CreateOrder()
    {
        Client client = await GetClientInstanceAsync();
        if (client.ClientBucket.Products.Count <= 0)
            return BadRequest("Client products bucket is empty");


        Order order = new Order()
        {
            Client = client,
            ClientId = client.Id,
        };

        order.Products.AddRange(client.ClientBucket.Products);
        order.Statuses.Enqueue(new Status { Name = "new" });
        client.Orders.Add(order);

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("bucket/add/product/{productId}")]
    public async Task<IActionResult> AddProductToBucket(ulong productId)
    {
        Client client = await GetClientInstanceAsync();
        Product? product = await _productShowcaseService.GetProductById(productId);
        if (product == null)
            return NotFound("Product not found");
        if (client.ClientBucket.Products.Contains(product))
            return BadRequest("Product already exists");

        client.ClientBucket.Products.Add(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    private async Task<Client> GetClientInstanceAsync()
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Client client = await _clientManager.Users
            .Include(x => x.Orders)
            .Include(x => x.ClientBucket)
            .Include(x => x.FavouritesBucket)
            .Include(x => x.Organization)
            .FirstAsync(x => x.Id == clientId);
        return client;
    }
}