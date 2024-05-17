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
[Route("market/client/")]
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

    [HttpGet("me/info")]
    public async Task<IActionResult> GetInfo()
    {
        Client client = await GetCurrentUserAsync();
        ClientInfoDto clientInfoDto = new ClientInfoDto
        {
            Name = client.AccountInfo.Name,
            Surname = client.AccountInfo.Surname,
            Country = client.AccountInfo.Geolocation.Country,
            City = client.AccountInfo.Geolocation.City,
            AddressInCity = client.AccountInfo.Geolocation.LocalAddress,
            FullAddress = client.AccountInfo.Geolocation.FullAddress
        };

        return Ok(JsonConvert.SerializeObject(clientInfoDto));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetSelf()
    {
        return Ok(JsonConvert.SerializeObject(await GetCurrentUserAsync()));
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

    [HttpGet("my/orders/{orderId}")]
    public async Task<IActionResult> GetOrder(ulong orderId)
    {
        Client client = await GetCurrentUserAsync();
        Order? order = client.Orders.FirstOrDefault(x => x.Id == orderId);
        if (order == null)
            return NotFound("Order not found");
        return Ok(JsonConvert.SerializeObject(order));
    }

    [HttpGet("my/orders/{orderId}/products")]
    public async Task<IActionResult> GetAllOrderProducts(ulong orderId)
    {
        Client client = await GetCurrentUserAsync();

        Order? order = client.Orders.FirstOrDefault(x => x.Id == orderId);
        if (order == null)
            return NotFound("Order not found");
        var productIds = order.Products
            .Select(product => product.Id)
            .ToList();

        return Ok(JsonConvert.SerializeObject(productIds));
    }

    [HttpGet("my/bucket/")]
    public async Task<IActionResult> GetProductsInBucket()
    {
        Client client = await GetCurrentUserAsync();
        var productIds = client.ClientBucket.Products
            .Select(product => product.Id)
            .ToList();
        return Ok(JsonConvert.SerializeObject(productIds));
    }

    [HttpPatch("my/bucket/add/product/{productId}")]
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

    [HttpPatch("my/bucket/remove/product/{productId}")]
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

    [HttpPost("my/orders/create")]
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
    [HttpPost("my/order/select-supplier/")]
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

    [HttpPost("my/orders/{orderId}/submit")]
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

        DateTime currentTime = DateTime.Now;

        var expirationTime = currentTime.AddDays(1)
            .AddHours(12)
            .AddMinutes(30);

        order.ExpirationTime = expirationTime;
        return Ok();
    }


 

    [HttpGet("my/favourite-products")]
    public async Task<IActionResult> GetFavouritesProducts()
    {
        Client client = await GetCurrentUserAsync();
        var productIds = client.FavouritesBucket.FavouriteProducts
            .Select(product => product.Id)
            .ToList();

        return Ok(JsonConvert.SerializeObject(productIds));
    }

    private async Task<Client> GetCurrentUserAsync()
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Client client = await _clientManager.FindByIdAsync(clientId);
        return client;
    }
}