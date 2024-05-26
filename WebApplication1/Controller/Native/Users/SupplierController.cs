using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Order;
using WebApplication1.Data.dao.Product;
using WebApplication1.Data.dao.Supplier;
using WebApplication1.Data.Dto;

namespace WebApplication1.Controller.Native.Users;

[ApiController]
[Route("supplier/")]
[Authorize(Roles = "supplier")]
public class SupplierController : ControllerBase
{
    private readonly UserManager<Supplier> _supplierManager;
    private readonly DbContext _context;

    public SupplierController(DbContext context, UserManager<Supplier> supplierManager)
    {
        _context = context;
        _supplierManager = supplierManager;
    }


    [HttpPost("orders/{orderId}/accept")]
    public async Task<IActionResult> AcceptOrder(ulong orderId)
    {
        Status acceptedStatus = new Status { Name = "accepted" };
        var order = await _context.Orders.Include(order => order.Statuses)
            .FirstOrDefaultAsync(order => order.Id == orderId);
        if (order == null)
            return NotFound("Order not found");

        if (order.Statuses.Contains(acceptedStatus))
            return BadRequest("Order is already performing");

        if (order.Statuses.Last().Name != "submitted")
            return BadRequest("Order cannot be accepted. The order status is invalid");
        
        Supplier supplier = await GetCurrentUserAsync();

        order.Supplier = supplier;
        order.SupplierId = supplier.Id;

        order.Statuses.Enqueue(acceptedStatus);
        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpPost("materials/{materialId}/add")]
    public async Task<IActionResult> AddMaterial(ulong materialId)
    {
        Material? material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return BadRequest("Material not found");

        var supplier = await GetCurrentUserAsync();
        supplier.Materials.Add(material);

        await _context.SaveChangesAsync();
        return Ok();
    }


    private async Task<Supplier> GetCurrentUserAsync()
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Supplier client = await _supplierManager.FindByIdAsync(clientId);
        return client;
    }
}