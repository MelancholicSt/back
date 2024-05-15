using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.dao.Product;

namespace WebApplication1.Controller;

[ApiController]

public class SupplierController : ControllerBase
{
    private UserManager<Account> _userManager;
    private DbContext _context;

    public SupplierController(UserManager<Account> userManager, DbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost("supplier/offers/create")]
    public async Task<IActionResult> AddProduct()
    {
        
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("supplier/offers/remove/{offerId}")]
    public async Task<IActionResult> RemoveOffer()
    {
        return Ok();
    }

    [HttpPost("supplier/offers/{offerId}/product/add/")]
    public async Task<IActionResult> AddProduct(long offerId, [FromBody] Product product)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Account account = await _userManager.FindByIdAsync(userId);
        if (account == null)
            return BadRequest("User not found");
       
       
        await _context.SaveChangesAsync();
        return Ok();
    }
}