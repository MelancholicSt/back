using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;

namespace WebApplication1.Controller.OpenApi;

[ApiController]
[Route("categories/")]
public class CategoryController : ControllerBase
{
    private DbContext _context;

    public CategoryController(DbContext context)
    {
        _context = context;
    }

    [HttpGet("{categoryId}/")]
    public async Task<IActionResult> GetCategory(ulong categoryId)
    {
        var category = await _context.Categories.Include(category => category.Products)
            .FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");
        var categoryProductIds = category.Products.Select(product => product.Id).ToList();
        var categoryChildrenIds = category.Children.Select(child => child.Id).ToList();

        var response = new
        {
            category.Name,
            ProductIds = categoryProductIds,
            ChildrenIds = categoryChildrenIds
        };

        return Ok(JsonConvert.SerializeObject(response));
    }

    [HttpGet("leafs")]
    public async Task<IActionResult> GetAllLeafCategories()
    {
        var leafs = _context.Categories
            .Where(category => !category.Children.Any())
            .Select(category => new { id = category.Id})
            .ToList();

        return Ok(JsonConvert.SerializeObject(leafs));
    }

    [HttpGet("{categoryId}/leafs")]
    public async Task<IActionResult> GetCategoryLeafs(ulong categoryId)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");

        var categoryLeafs = category.Children.Where(leaf => !leaf.Children.Any()).ToList();
        var leafsIds = categoryLeafs.Select(leaf => leaf.Id).ToList();
        return Ok(JsonConvert.SerializeObject(leafsIds));
    }

    [HttpPatch("{categoryId}/rename")]
    public async Task<IActionResult> EditCategoryName(ulong categoryId, string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");

        category.Name = name;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{categoryId}/add/products")]
    public async Task<IActionResult> AddProductToCategory(ulong categoryId, ulong productId)
    {
        var category = await _context.Categories
            .Include(category => category.Products)
            .FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");

        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);

        if (product == null)
            return NotFound("Product not found");
        if (category.Products.Contains(product))
            return BadRequest("Product already exists in this category");

        category.Products.Add(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{categoryId}/remove/products")]
    public async Task<IActionResult> RemoveProductsFromCategory(ulong categoryId, [FromBody] List<ulong> productIds)
    {
        if (!productIds.Any())
            return BadRequest("No product ids entered");
        
        var category = await _context.Categories
            .Include(category => category.Products)
            .FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");

        var currentProductIds = _context.Products
            .Select(product => product.Id)
            .Where(product => productIds.Contains(product));

        if (!currentProductIds.SequenceEqual(productIds))
        {
            var notFoundProductIds = productIds.Except(currentProductIds);
            return NotFound($"Those product ids not found: {string.Join("\n", notFoundProductIds)}");
        }
        
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory(string name, ulong? parentId)
    {
        var categoryNames = _context.Categories.Select(category => category.Name.ToLower());
        if (categoryNames.Contains(name))
            return BadRequest($"Category with name {name} already exists");

        Category category = new Category()
        {
            Name = name
        };
        if (parentId != null)
        {
            var parentCategory = await _context.Categories
                .Include(category => category.Children)
                .Include(category => category.Products)
                .FirstOrDefaultAsync(category => category.Id == parentId);

            if (parentCategory == null)
                return NotFound("Parent category not found");

            parentCategory.Children.Add(category);
            return Ok();
        }

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return Ok();
    }
}