using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebApplication1.Controller;

[ApiController]
public class ManagerController : ControllerBase
{
    private UserManager<Account> _userManager;
    private DbContext _context;

    public ManagerController(UserManager<Account> userManager, DbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet("manager/users/get/{userId}/orders/")]
    public async Task<IActionResult> GetUserOrders(string userId)
    {
        Account? account = await _userManager.FindByIdAsync(userId);
        if (account == null)
            return Problem("Something got wrong in ClientController.GetAllProducts()");
        var orders = _context.UserOrders
            .Include(x => x.Products)
            .Where(x => x.OwnerId == account.Id);

        return Ok(JsonConvert.SerializeObject(orders.ToList()));
    }

    [HttpGet("manager/suppliers/{supplierId}/offers/get")]
    public async Task<IActionResult> GetSupplierOffers(string supplierId)
    {
        var user = await _userManager.FindByIdAsync(supplierId);
        if (user == null)
            return BadRequest("Supplier not found");
        if (!await _userManager.IsInRoleAsync(user, "supplier"))
            return BadRequest("User is not supplier");
        var offers = _context.Offers
            .Include(x => x.Products)
            .Where(x => x.OwnerId == user.Id);
        return Ok(JsonConvert.SerializeObject(offers));
    }

    [HttpGet("manager/suppliers/{supplierId}/offers/get/{offerId}")]
    public async Task<IActionResult> GetSupplierOffer(string supplierId, long offerId)
    {
        var user = await _userManager.FindByIdAsync(supplierId);
        if (user == null)
            return BadRequest("Supplier not found");
        if (!await _userManager.IsInRoleAsync(user, "Supplier"))
            return BadRequest("User is not supplier");
        var offer = _context.Offers.Include(x => x.Products).FirstOrDefault(x => x.OfferId == offerId);
        if (offer == null)
            return BadRequest("Offer not found");
        return Ok(JsonConvert.SerializeObject(offer));
    }

    [HttpPost("manager/products/{productId}/change/price/{pricePerQuantity}")]
    public async Task<IActionResult> ChangePrice(long productId, int pricePerQuantity)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            return BadRequest("Product not found");
        product.PricePerQuantity = pricePerQuantity;
        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPut("manager/users/add/supplier/{userId}")]
    public async Task<IActionResult> AddSupplier(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return BadRequest("user not found");
        await _userManager.AddToRoleAsync(user, "Supplier");
        return Ok();
    }

    [HttpPut("manager/users/remove/supplier/{userId}")]
    public async Task<IActionResult> RemoveSupplier(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return BadRequest("User not found");
        await _userManager.RemoveFromRoleAsync(user, "Supplier");
        return Ok();
    }

    [HttpGet("manager/users/suppliers/get")]
    public async Task<IActionResult> GetALlSuppliers()
    {
        List<Account>? suppliers = new List<Account>();
        await _userManager.Users.ForEachAsync(async x =>
        {
            if (await _userManager.IsInRoleAsync(x, "supplier"))
                suppliers.Add(x);
        });

        return Ok(JsonConvert.SerializeObject(suppliers));
    }
}