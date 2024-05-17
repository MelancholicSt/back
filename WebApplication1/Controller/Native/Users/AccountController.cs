using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Identity;
using WebApplication1.Data.Dto;
using WebApplication1.Service.ImageService;

namespace WebApplication1.Controller;

[ApiController]
[Route("market/account/")]
public class AccountController : ControllerBase
{
    private SignInManager<Account> _signInManager;
    private RoleManager<IdentityRole> _roleManager;
    private IEmailSender _emailSender;
    private ILogger<AccountController> _logger;
    private ICloudImageService _imageService;
    private DbContext _context;

    public AccountController(SignInManager<Account> signInManager, IEmailSender emailSender,
        UserManager<Account> userManager, ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager,
        ICloudImageService imageService, DbContext context)
    {
        _signInManager = signInManager;
        _emailSender = emailSender;
        _logger = logger;
        _roleManager = roleManager;
        _imageService = imageService;
        _context = context;
    }

    [HttpPost("account/register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] AuthAccountDto account)
    {
        _logger.LogInformation(
            $"{DateTime.Now}: Register endpoint invoked by {HttpContext.Connection.RemoteIpAddress}");
        _logger.LogInformation($"{DateTime.Now} Trying to create user");

        Account? user = new Account
        {
            Email = account.Email,
            UserName = account.Username,
            Organization = new Organization
            {
                Name = "Not chosen"
            }
        };

        var result = await _signInManager.UserManager.CreateAsync(user, account.Password);

        if (!result.Succeeded)
        {
            _logger.LogError("Result is not succeeded. Got problem while creating user");
            foreach (var error in result.Errors)
                _logger.LogTrace(error.Description);
            return BadRequest();
        }

        user = await _signInManager.UserManager.FindByEmailAsync(user.Email);
        _logger.LogInformation(
            $"User has been creating in: {DateTime.Now} by {HttpContext.Connection.RemoteIpAddress}");

        _logger.LogInformation("");

        var code = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);

        var redirectLink = Url.Action(
            "ConfirmEmail", "Account",
            new
            {
                code = code,
                userId = user.Id
            },
            HttpContext.Request.Scheme);
        _logger.LogInformation($"{DateTime.Now}: Sending email to {user.Email}");
        await _emailSender.SendEmailAsync(user.Email, "Marketplace",
            $" <a href=\"{redirectLink}\">link</a>");
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginAccountDto authAccount)
    {
        if (_signInManager.IsSignedIn(User))
        {
            return Ok();
        }

        var user = await _signInManager.UserManager.FindByNameAsync(authAccount.Username);
        if (user == null)
            return BadRequest("Username or password is incorrect");

        var result = await _signInManager.PasswordSignInAsync(authAccount.Username, authAccount.Password, false, false);

        if (!result.Succeeded)
            return BadRequest("Bad credentials");
        return Ok("Succeeded!");
    }
    
    [HttpPost("image/set")]
    [Authorize("Confirmed")]
    public async Task<IActionResult> SetAccountImage(IFormFile file)
    {
        await _imageService.UploadFileAsync(file, "account");
        var userId = User.FindFirstValue("uid");

        var user = await _signInManager.UserManager.Users.FirstAsync(x => x.Id == userId);

        user.ProfileImage = await _context.Images.FirstAsync(x => x.Name == file.Name);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("info")]
    [Authorize("Confirmed")]
    public async Task<IActionResult> GetAccountInfo()
    {
        var userId = User.FindFirstValue("uid");
        var account = await _signInManager.UserManager.FindByIdAsync(userId);

        return Ok(JsonConvert.SerializeObject(account.AccountInfo));
    }
    /// <summary>
    ///  asdfasdvascva
    /// </summary>
    /// <param name="code"></param>
    /// <param name="userId"></param>
    ///
    /// <returns>asdfavasc</returns>
    [HttpGet("confirm-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string? code, string? userId)
    {
        if (userId == null || code == null)
        {
            _logger.LogWarning("User id or confirmation code is null");
            return BadRequest("Bad credentials");
        }

        var user = await _signInManager.UserManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogError("User id is incorrect");
            return BadRequest("Bad credentials");
        }

        var result = await _signInManager.UserManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to confirm email");
            return this.Problem("Confirmation failed");
        }

        await _signInManager.UserManager.AddClaimAsync(user, new Claim("confirmedEmail", "true"));

        Response.Cookies.Append("uid", user.Id);

        return Ok($"Email: {user.Email} has been confirmed");
    }
}