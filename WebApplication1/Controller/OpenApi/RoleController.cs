using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Data.dao.Identity;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<Account> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<Account> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet("all")]
    public IActionResult GetAllRoles()
    {
        var roles = _roleManager.Roles.ToList();
        return Ok(JsonConvert.SerializeObject(roles));
    }

    [HttpGet("{name}/users")]
    public async Task<IActionResult> GetUsersInRole(string name)
    {
        if (await _roleManager.FindByNameAsync(name) == null)
            return NotFound("Role not found");

        var users = _userManager.Users.Where(user => _userManager.IsInRoleAsync(user, name).Result);
        return Ok(JsonConvert.SerializeObject(users.ToList()));
    }
}