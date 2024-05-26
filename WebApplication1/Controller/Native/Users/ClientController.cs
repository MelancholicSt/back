using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Client;
using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.dao.Order;
using WebApplication1.Data.dao.Product;
using WebApplication1.Data.dao.Supplier;
using WebApplication1.Data.Dto;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]
[Route("client/")]
public class ClientController : ControllerBase
{
    private readonly SignInManager<Client> _clientSignInManager;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<Supplier> _supplierManager;
    private INotificationSender _notificationSender;

    private DbContext _context;
    private ILogger<ClientController> _logger;

    public ClientController(SignInManager<Client> clientSignInManager, DbContext context,
        INotificationSender notificationSender,
        UserManager<Supplier> supplierManager, IEmailSender emailSender, ILogger<ClientController> logger)
    {
        _clientSignInManager = clientSignInManager;
        _context = context;
        _notificationSender = notificationSender;
        _supplierManager = supplierManager;
        _emailSender = emailSender;
        _logger = logger;
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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewClient([FromBody] AuthAccountDto account)
    {
        Client? client = new Client
        {
            Email = account.Email,
            UserName = account.Username,
            Organization = new Organization
            {
                Name = "Unassigned"
            },
            AccountInfo = new AccountInfo
            {
                Name = "Test",
                Surname = "Lol"
            }
        };
        var result = await _clientSignInManager.UserManager.CreateAsync(client, account.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        client = await _clientSignInManager.UserManager.FindByEmailAsync(account.Email);
        
        var code = await _clientSignInManager.UserManager.GenerateEmailConfirmationTokenAsync(client);
        
        var redirectLink = Url.Action(
            "ConfirmEmail", "Account",
            new
            {
                code = code,
                userId = client.Id
            },
            HttpContext.Request.Scheme);
        
        await _emailSender.SendEmailAsync(client.Email, "Marketplace",
            $" <a href=\"{redirectLink}\">link</a>");
        
        return Ok();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string? code, string? userId)
    {
        if (userId == null || code == null)
        {
            _logger.LogWarning("User id or confirmation code is null");
            return BadRequest("Bad credentials");
        }

        var client = await _clientSignInManager.UserManager.FindByIdAsync(userId);
        if (client == null)
        {
            _logger.LogError("User id is incorrect");
            return BadRequest("Bad credentials");
        }

        var result = await _clientSignInManager.UserManager.ConfirmEmailAsync(client, code);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to confirm email");
            return Problem("Confirmation failed");
        }

        await _clientSignInManager.UserManager.AddClaimAsync(client, new Claim("confirmedEmail", "true"));

        Response.Cookies.Append("uid", client.Id);
        return Ok($"Email: {client.Email} has been confirmed");
    }

    [HttpGet("bucket/")]
    public async Task<IActionResult> GetProductsInBucket()
    {
        Client client = await GetCurrentUserAsync();
        var productIds = client.ClientBucket.Materials
            .Select(product => product.Id)
            .ToList();
        return Ok(JsonConvert.SerializeObject(productIds));
    }

    [HttpPatch("bucket/{materialId}/add")]
    public async Task<IActionResult> AddProductToBucket(ulong materialId)
    {
        Client client = await GetCurrentUserAsync();
        Material? material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Product not found");
        if (client.ClientBucket.Materials.Contains(material))
            return BadRequest("Product already exists");

        client.ClientBucket.Materials.Add(material);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("bucket/{materialId}/remove/")]
    public async Task<IActionResult> RemoveProductFromBucket(ulong materialId)
    {
        Client client = await GetCurrentUserAsync();
        Material? product = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (product == null)
            return NotFound("Product not found");

        if (!client.ClientBucket.Materials.Contains(product))
            return BadRequest("Product doesn't exists in bucket");

        client.ClientBucket.Materials.Remove(product);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("orders/create")]
    public async Task<IActionResult> CreateOrder()
    {
        Client client = await GetCurrentUserAsync();
        if (client.ClientBucket.Materials.Count <= 0)
            return BadRequest("Client products bucket is empty");

        Order order = new Order
        {
            Client = client,
            ClientId = client.Id,
        };

        order.Materials.AddRange(client.ClientBucket.Materials);
        order.Statuses.Enqueue(new Status { Name = "new" });
        client.Orders.Add(order);
        client.ClientBucket.Materials.Clear();
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
        order.Statuses.Enqueue(new Status() { Name = "submitted" });
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

    [HttpPatch("favourites/{materialId}/add/")]
    public async Task<IActionResult> AddProductToFavourites(ulong materialId)
    {
        Client client = await GetCurrentUserAsync();
        var product = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (product == null)
            return NotFound("Product not found");

        client.FavouritesBucket.FavouriteProducts.Add(product);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private async Task<Client> GetCurrentUserAsync()
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Client client = await _clientSignInManager.UserManager.FindByIdAsync(clientId);
        return client;
    }
}