using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Product;

namespace WebApplication1.Controller.OpenApi;

[ApiController]
[Route("api/categories/")]
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
        var category = await _context.Categories.Include(category => category.Materials)
            .FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");
        var categoryProductIds = category.Materials.Select(product => product.Id).ToList();
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
            .Select(category => new { id = category.Id })
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

    [HttpPost("{categoryId}/add/materials")]
    public async Task<IActionResult> AddMaterialToCategory(ulong categoryId, [FromBody] List<ulong> materialIds)
    {
        var category = await _context.Categories
            .Include(category => category.Materials)
            .FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");

        List<Material> materials = new List<Material>();
        List<Material> notFoundMaterials = new List<Material>();
        foreach (ulong materialId in materialIds)
        {
            var material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
            if (material == null)
            {
                notFoundMaterials.Add(material);
                continue;
            }
            if(category.Materials.Contains(material))
                continue;
            
            materials.Add(material);
        }

        
        category.Materials.AddRange(materials);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{categoryId}/remove/materials")]
    public async Task<IActionResult> RemoveMaterialsFromCategory(ulong categoryId, [FromBody] List<ulong> meterialIds)
    {
        if (!meterialIds.Any())
            return BadRequest("No product ids entered");

        var category = await _context.Categories
            .Include(category => category.Materials)
            .FirstOrDefaultAsync(category => category.Id == categoryId);
        if (category == null)
            return NotFound("Category not found");

        var currentMaterialIds = _context.Materials
            .Select(material => material.Id)
            .Where(material => meterialIds.Contains(material));

        if (!currentMaterialIds.SequenceEqual(meterialIds))
        {
            var notFoundProductIds = meterialIds.Except(currentMaterialIds);
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
                .Include(category => category.Materials)
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