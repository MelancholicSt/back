using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.dao.Supplier;

namespace WebApplication1.Controller.Native.Users;

[ApiController]
[Route("manager/")]
[Authorize(Roles = "manager")]
public class ManagerController : ControllerBase
{
    private UserManager<Supplier> _userManager;

    private DbContext _context;

    public ManagerController(UserManager<Supplier> userManager, DbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost("suppliers/add")]
    public async Task<IActionResult> AddSupplier(string accountId)
    {
        Supplier? user = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == accountId);
        if (user == null)
            return NotFound("User not found");

        if (await _userManager.IsInRoleAsync(user, "supplier"))
            return BadRequest("User already is supplier");
        await _userManager.AddToRoleAsync(user, "supplier");
        _context.Suppliers.Add(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("suppliers/get")]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var suppliersIds = _userManager.Users
            .Where(user => _userManager.IsInRoleAsync(user, "supplier").Result)
            .Select(supplier => supplier.Id).ToList();
        
        return Ok(JsonConvert.SerializeObject(suppliersIds));
    }
}