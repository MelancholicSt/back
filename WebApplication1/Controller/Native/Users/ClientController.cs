using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Client;
using WebApplication1.Data.dao.Order;
using WebApplication1.Data.dao.Product;
using WebApplication1.Data.dao.Supplier;
using WebApplication1.Data.Dto;
using WebApplication1.ProductShowcaseService;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]
[Route("client/")]
[Authorize(Roles = "client")]
public class ClientController : ControllerBase
{
    private UserManager<Client> _clientManager;
    private UserManager<Supplier> _supplierManager;
    private INotificationSender _notificationSender;
    private IProductShowcaseService _productShowcaseService;
    private DbContext _context;

    public ClientController(UserManager<Client> clientManager, DbContext context,
        INotificationSender notificationSender, IProductShowcaseService productShowcaseService,
        UserManager<Supplier> supplierManager)
    {
        _clientManager = clientManager;
        _context = context;
        _notificationSender = notificationSender;
        _productShowcaseService = productShowcaseService;
        _supplierManager = supplierManager;
    }
    
    /// <summary>
    /// Receives all orders of requesting client 
    /// </summary>
    /// <returns></returns>
    [HttpGet("orders/all")]
    public async Task<IActionResult> GetAllOrders()
    {
        Client client = await GetCurrentUserAsync();
        var orderIds = client.Orders
            .Select(order => order.Id)
            .ToList();
        return Ok(JsonConvert.SerializeObject(orderIds));
    }
    

    [HttpGet("bucket/")]
    public async Task<IActionResult> GetProductsInBucket()
    {
        Client client = await GetCurrentUserAsync();
        var productIds = client.ClientBucket.Products
            .Select(product => product.Id)
            .ToList();
        return Ok(JsonConvert.SerializeObject(productIds));
    }

    [HttpPatch("bucket/add/{productId}")]
    public async Task<IActionResult> AddProductToBucket(ulong productId)
    {
        Client client = await GetCurrentUserAsync();
        Product? product = await _productShowcaseService.GetProductById(productId);
        if (product == null)
            return NotFound("Product not found");
        if (client.ClientBucket.Products.Contains(product))
            return BadRequest("Product already exists");

        client.ClientBucket.Products.Add(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("bucket/remove/{productId}")]
    public async Task<IActionResult> RemoveProductFromBucket(ulong productId)
    {
        Client client = await GetCurrentUserAsync();
        Product? product = await _productShowcaseService.GetProductById(productId);
        if (product == null)
            return NotFound("Product not found");

        if (!client.ClientBucket.Products.Contains(product))
            return BadRequest("Product doesn't exists in bucket");

        client.ClientBucket.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("orders/create")]
    public async Task<IActionResult> CreateOrder()
    {
        Client client = await GetCurrentUserAsync();
        if (client.ClientBucket.Products.Count <= 0)
            return BadRequest("Client products bucket is empty");

        Order order = new Order
        {
            Client = client,
            ClientId = client.Id,
        };

        order.Products.AddRange(client.ClientBucket.Products);
        order.Statuses.Enqueue(new Status { Name = "new" });
        client.Orders.Add(order);
        client.ClientBucket.Products.Clear();
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpPost("order/select-supplier/")]
    public async Task<IActionResult> SelectSupplierForOrder(string supplierId)
    {
        Supplier? supplier = await _supplierManager.FindByIdAsync(supplierId);
        if (!_supplierManager.Users.Contains(supplier))
            return NotFound("Supplier not found");
        
        var orderId = Convert.ToUInt64(User.FindFirstValue("current-order"));
        Order? order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == orderId);
        
        if (order == null)
            return NotFound("Order not found");
        
        return Ok();
    }

    [HttpPost("orders/{orderId}/submit")]
    public async Task<IActionResult> SubmitOrder(ulong orderId)
    {
        Client client = await GetCurrentUserAsync();
        Order? order = client.Orders.FirstOrDefault(order => order.Id == Convert.ToUInt64(orderId));
        if (order == null)
            return NotFound("Order not found");

        if (!client.Orders.Contains(order))
            return NotFound("Order not found in client orders list");

        if (order.Supplier == null)
            return BadRequest("Supplier isn't selected for this order");

        if (order.Statuses.Last().Name.ToLower() != "new")
            return BadRequest($"Cannot submit order with {order.Statuses.Last()} status");
        
        DateTime currentTime = DateTime.Now;
        
        var expirationTime = currentTime.AddDays(1)
            .AddHours(12)
            .AddMinutes(30);
        
        order.ExpirationTime = expirationTime;
        order.Statuses.Enqueue(new Status() { Name = "submitted"});
        return Ok();
    }

    [HttpGet("favourites")]
    public async Task<IActionResult> GetFavouritesProducts()
    {
        Client client = await GetCurrentUserAsync();
        var productIds = client.FavouritesBucket.FavouriteProducts
            .Select(product => product.Id)
            .ToList();

        return Ok(JsonConvert.SerializeObject(productIds));
    }

    [HttpPatch("favourites/add/{productId}")]
    public async Task<IActionResult> AddProductToFavourites(ulong productId)
    {
        Client client = await GetCurrentUserAsync();
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);
        if (product == null)
            return NotFound("Product not found");
        
        client.FavouritesBucket.FavouriteProducts.Add(product);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private async Task<Client> GetCurrentUserAsync()
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Client client = await _clientManager.FindByIdAsync(clientId);
        return client;
    }
}