using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao.Product;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private DbContext _context;

    public ProductController(DbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public IActionResult GetAllProducts()
    {
        return Ok(JsonConvert.SerializeObject(_context.Products.ToList()));
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(ulong productId)
    {
        Product? product = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);
        if (product == null)
            return NotFound("Product not found");
        return Ok(JsonConvert.SerializeObject(product));
    }

    [HttpGet("{productId}/information")]
    public async Task<IActionResult> GetProductInfo(ulong productId)
    {
        Product? product = await _context.Products.Include(product => product.Info)
            .FirstOrDefaultAsync(product => product.Id == productId);
        if (product == null)
            return NotFound("Order not found");

        return Ok(JsonConvert.SerializeObject(product.Info));
    }
    
}