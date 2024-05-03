using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.Dto;

namespace WebApplication1.Controller;

[ApiController]
public class ProductCrudController : ControllerBase
{
    private DbContext _context;

    public ProductCrudController(DbContext context)
    {
        _context = context;
    }

    [HttpGet("products/get/all")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(JsonConvert.SerializeObject(_context.Products.ToList()));
    }

    [HttpGet("products/get/{productId}")]
    public async Task<IActionResult> GetProduct(long productId)
    {
        var product = _context.Products.FirstOrDefault();
        if (product == null)
            return BadRequest("Product not found");
        return Ok(JsonConvert.SerializeObject(product));
    }

    [HttpGet("products/get/in-stock")]
    public async Task<IActionResult> GetProductsInStock()
    {
        var products = _context.Products.Where(x => x.Quantity > 0);
        if (products == null)
            return BadRequest("All products out of stock");
        return Ok(JsonConvert.SerializeObject(products.ToList()));
    }

    [HttpPost("products/add-product")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        var exists = _context.Products.FirstOrDefault(x => x.Name == productDto.Name) == null;
        if (exists)
            return BadRequest("Product already exists");
      
      
        Product product = new Product()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Measure = new Measure()
            {
                MeasureName = productDto.MeasureName
            },
            PricePerQuantity = productDto.PricePerQuantity,
            Quantity = productDto.Quantity
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("products/{productId}/add/amount/{productAmount}")]
    public async Task<IActionResult> AddProductAmount(long productId, int productAmount)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            return BadRequest("Product not found");
        product.Quantity += productAmount;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("products/{productId}/set/amount/{productAmount}")]
    public async Task<IActionResult> SetProductAmount(long productId, int productAmount)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            return BadRequest("Product not found");
        product.Quantity = productAmount;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("products/{productId}/change")]
    public async Task<IActionResult> ChangeProduct(long productId, [FromBody] ProductDto productDto)
    {
        var oldProduct = _context.Products.FirstOrDefault(x => x.Id == productId);
        if (oldProduct == null)
            return BadRequest("Product not found");
        _context.Products.Remove(oldProduct);
        Product product = new Product()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Measure = new Measure()
            {
                MeasureName = productDto.MeasureName
            },
            PricePerQuantity = productDto.PricePerQuantity,
            Id = productId,
            Quantity = productDto.Quantity
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("products/{productId}/delete")]
    public async Task<IActionResult> DeleteProduct(long productId)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            return BadRequest("Product not found");
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok();
    }
}