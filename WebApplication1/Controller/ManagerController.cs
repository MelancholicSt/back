using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;

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

    [HttpPost("manager/suppliers/add")]
    public async Task<IActionResult> AddSupplier(string accountId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == accountId);
        if (user == null)
            return NotFound("User not found");
        if (await _userManager.IsInRoleAsync(user, "supplier"))
            return BadRequest("User already is supplier");
        await _userManager.AddToRoleAsync(user, "supplier");
        return Ok();
    }
}