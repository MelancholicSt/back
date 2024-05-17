using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Order;
using WebApplication1.Data.dao.Product;
using WebApplication1.Data.dao.Product.Details;
using WebApplication1.Data.dao.Supplier;
using WebApplication1.Data.Dto;

namespace WebApplication1.Controller.Native.Users;

[ApiController]
[Route("supplier/")]
public class SupplierController : ControllerBase
{
    private UserManager<Supplier> _supplierManager;
    private DbContext _context;

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
            return BadRequest("Order is already accepted");

        Supplier supplier = await GetCurrentUserAsync();

        order.Supplier = supplier;
        order.SupplierId = supplier.Id;

        order.Statuses.Enqueue(acceptedStatus);
        await _context.SaveChangesAsync();
        
        return Ok();
    }


    [HttpPost("products/add")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        Supplier supplier = await GetCurrentUserAsync();

        Material? material =
            await _context.Materials.FirstOrDefaultAsync(material => material.Name == productDto.MaterialName);
        Category? category =
            await _context.Categories.FirstOrDefaultAsync(category => category.Name == productDto.CategoryName);

        if (material == null)
            return NotFound($"Material \"{productDto.MaterialName}\" not found");

        if (category == null)
            return NotFound($"Category \"{productDto.CategoryName}\" not found");

        var supplierMaterialNames = supplier.AvailableProducts.Select(material => material.Name).ToList();

        if (supplierMaterialNames.Contains(productDto.MaterialName))
            return BadRequest("Supplier is selling this material already");

        Product product = new Product
        {
            Name = productDto.Name,
            Category = category,
            CategoryId = category.Id,
            Supplier = supplier,
            SupplierId = supplier.Id,

        };
        await _context.Products.AddAsync(product);
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