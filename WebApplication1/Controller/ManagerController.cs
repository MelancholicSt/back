using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.Identity;

namespace WebApplication1.Controller;

[ApiController]
[Authorize(Roles="manager")]
public class ManagerController : ControllerBase
{
    private UserManager<Account> _userManager;
    private DbContext _context;

    public ManagerController(UserManager<Account> userManager, DbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet("manager/clients/{userId}/orders/get/all")]
    public async Task<IActionResult> GetClientOrders(string userId)
    {
        Account? account = await _userManager.FindByIdAsync(userId);
        if (account == null)
            return BadRequest("Account not found");
        
        var orders = _context.Orders
            .Include(x => x.Products)
            .Where(x => x.OwnerId == account.Id);

        return Ok(JsonConvert.SerializeObject(orders.ToList()));
    }

    [HttpGet("manager/clients/{userId}/orders/get/{orderId}")]
    public async Task<IActionResult> GetClientOrder(string userId, long orderId)
    {
        
    }

    [HttpGet("manager/suppliers/{supplierId}/products/get/all")]
    public async Task<IActionResult> GetSupplierProducts(string supplierId)
    {
        var user = await _userManager.FindByIdAsync(supplierId);
        if (user == null)
            return BadRequest("Account not found");
        if (!await _userManager.IsInRoleAsync(user, "supplier"))
            return BadRequest("Supplier not found. Account exists, but he is not supplier");

        var supplier = _context.Suppliers
            .Include(x => x.OfferingProducts)
            .FirstOrDefault(x => x.Id == supplierId)!;
        
        return Ok(JsonConvert.SerializeObject(supplier.OfferingProducts));
    }

    [HttpGet("manager/suppliers/{supplierId}/products/get/{productId}")]
    public async Task<IActionResult> GetSupplierOfferingProduct(string supplierId, long productId)
    {
        var user = await _userManager.FindByIdAsync(supplierId);
        if (user == null)
            return BadRequest("Supplier not found");
        if (!await _userManager.IsInRoleAsync(user, "Supplier"))
            return BadRequest("User is not supplier");
        var supplier = _context.Suppliers
            .Include(x => x.OfferingProducts)
            .FirstOrDefault(x => x.Id == supplierId)!;
        var product = supplier.OfferingProducts.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            return BadRequest("Supplier product not found");

        return Ok(JsonConvert.SerializeObject(product));
    }

    [HttpPost("manager/products/{productId}/change/price/{pricePerQuantity}")]
    public async Task<IActionResult> ChangePrice(long productId, int pricePerQuantity)
    {
        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPut("manager/users/add/supplier/{userId}")]
    public async Task<IActionResult> AddSupplier(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return BadRequest("user not found");
        if (await _userManager.IsInRoleAsync(user, "supplier"))
            return BadRequest("User already is supplier");

        await _userManager.AddToRoleAsync(user, "supplier");
        return Ok();
    }

    [HttpPut("manager/users/remove/supplier/{userId}")]
    public async Task<IActionResult> RemoveSupplier(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return BadRequest("User not found");
        if (!await _userManager.IsInRoleAsync(user, "supplier"))
            return BadRequest("User is not supplier");

        await _userManager.RemoveFromRoleAsync(user, "supplier");

        var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == userId);
        _context.Remove(supplier);

        return Ok();
    }

    [HttpGet("manager/users/suppliers/get")]
    public async Task<IActionResult> GetALlSuppliers()
    {
        var suppliers = _context.Suppliers.Include(x => x.OfferingMaterials)
            .Include(x => x.PerformingOrders)
            .Include(x => x.OfferingProducts).ToList();

        return Ok(JsonConvert.SerializeObject(suppliers));
    }
}