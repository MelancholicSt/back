using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Other;
using WebApplication1.Data.Dto;
using WebApplication1.Data.Identity;
using WebApplication1.Service;

namespace WebApplication1.Controller;

[ApiController]
public class ClientController : ControllerBase
{
    private UserManager<Account> _userManager;
    private INotificationService _notificationService;
    private DbContext _context;

    public ClientController(UserManager<Account> userManager, DbContext context,
        INotificationService notificationService)
    {
        _userManager = userManager;
        _context = context;
        _notificationService = notificationService;
    }

    [HttpGet("client/orders/get")]
    public async Task<IActionResult> GetAllOrders()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Account account = await _userManager.FindByIdAsync(id);

        if (account == null)
            return Problem("Something got wrong in ClientController.GetAllProducts()");
        var orders = _context.Orders.Where(c => c.OwnerId == account.Id).Include(c => c.Products);
        return Ok(JsonConvert.SerializeObject(orders.ToList()));
    }

    [HttpPost("client/orders/{orderId}/products/add")]
    public async Task<IActionResult> AddProduct(long orderId, [FromBody] ProductDto product)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id is null)
            return BadRequest("Problema blyat");
        var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
        if (order == null)
            return BadRequest("Order not found");
        var newProduct = new Product()
        {
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            PricePerQuantity = product.PricePerQuantity,
            Measure = new Measure()
            {
                Value = product.MeasureName
            }
        };
        order.Products.Add(newProduct);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpPost("client/orders/create")]
    public async Task<IActionResult> CreateOrder()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Account account = await _userManager.FindByIdAsync(userId);
        if (account == null)
            return BadRequest("User is null");
        Status? status = new Status()
        {
            Value = "new"
        };
        Order order = new Order
        {
            Products = new List<Product>(),
            Status = status,
            OwnerId = account.Id
        };
        account.Orders.Add(order);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("client/orders/{orderId}/products/{productId}/remove")]
    public async Task<IActionResult> RemoveProduct(long orderId, long productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Account account = await _userManager.FindByIdAsync(userId);
        if (account == null)
            return BadRequest("User not found");
        var order = _context.Orders.Include(x => x.Products).FirstOrDefault(x => x.Id == orderId);
        if (order == null)
            return BadRequest("Order not found");

        var product = order.Products.FirstOrDefault(x => x.ProductId == productId);
        if (product == null)
            return BadRequest("Product not found");
        order.Products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("client/orders/{orderId}/offers/{offerId}/select")]
    public async Task<IActionResult> SelectOffer(long orderId, long offerId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Account account = await _userManager.FindByIdAsync(userId);
        if (account == null)
            return BadRequest("User not found");
        var offer = _context.Offers.FirstOrDefault(x => x.OfferId == offerId);
        if (offer == null)
            return BadRequest("Offer not found");
        var order = _context.Orders.Include(x => x.Products).FirstOrDefault(x => x.Id == orderId);
        if (order == null)
            return BadRequest("Order not found");

        _context.ClientOffers.Add(new ClientOffer
        {
            Offer = offer,
            Order = order,
            Client = account
        });
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("client/orders/{orderId}/submit")]
    public async Task<IActionResult> SubmitOrder(long orderId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Account account = await _userManager.FindByIdAsync(userId);
        if (account == null)
            return BadRequest("User not found");

        var order = account.Orders.FirstOrDefault(x => x.Id == orderId);
        if (order == null)
            return BadRequest("Order not found");

        order.Status = new Status
        {
            Value = "Submitted"
        };
        order.Products.ForEach(x => order.TotalPrice += x.PricePerQuantity * x.Quantity);
        await _context.SaveChangesAsync();
        var code = Url.Action(
            "GetOrder",
            "OrderCrud",
            new { orderId });
        await _notificationService.NotifyAllManagersAsync($"New order. <a href={code}>Click to open order</a>");
        return Ok();
    }
}