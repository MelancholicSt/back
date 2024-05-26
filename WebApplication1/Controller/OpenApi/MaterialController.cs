using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Product;
using WebApplication1.Data.Dto;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/products")]
public class MaterialController : ControllerBase
{
    private DbContext _context;

    public MaterialController(DbContext context)
    {
        _context = context;
    }


    [HttpGet("{materialId}/")]
    public async Task<IActionResult> GetMaterial(ulong materialId)
    {
        Material? material = await _context.Materials
            .Include(material => material.Suppliers)
            .Include(material => material.MainImage)
            .Include(material => material.Images)
            .FirstOrDefaultAsync(product => product.Id == materialId);
        if (material == null)
            return NotFound("Order not found");

        var response = new
        {
            material.Name,
            material.Id,
            AvailableSuppliers = material.Suppliers.Select(supplier => supplier.Id),
            material.CategoryId,
            material.Description,
            MainImageGuid = material.MainImage?.Guid,
            Images = material.Images!.Select(image => image.Guid),
            material.Measure,
        };

        return Ok(JsonConvert.SerializeObject(response));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateMaterial([FromBody] MaterialDto materialDto)
    {
        Category? category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == materialDto.CategoryId);
        if (category == null)
            return NotFound("Category not found");
        bool isNameExists = await _context.Materials
            .Select(material => material.Name.ToLower())
            .FirstOrDefaultAsync(materialName => materialName == materialDto.Name.ToLower()) != null;
        if (isNameExists)
            return BadRequest("Name is already taken");
        
        Material material = new Material
        {
            Name = materialDto.Name,
            Description = materialDto.Description,
            Category = category,
            CategoryId = category.Id,
            Measure = materialDto.Measure
        };

        await _context.Materials.AddAsync(material);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{materialId}/set")]
    public async Task<IActionResult> ReplaceMaterial(ulong materialId, [FromBody] MaterialDto materialDto)
    {
        Material? material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Material not found");

        Category? category =
            await _context.Categories.FirstOrDefaultAsync(category => category.Id == materialDto.CategoryId);
        if (category == null)
            return NotFound("Category not found");
        
        material.Description = materialDto.Description;
        material.Category = category;
        material.CategoryId = category.Id;
        material.Name = materialDto.Name;
        material.Measure = materialDto.Measure;
        material.MainImage = await _context.Images.FirstOrDefaultAsync(image => image.Guid == materialDto.MainImageGuid);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{materialId}")]
    public async Task<IActionResult> DeleteMaterial(ulong materialId)
    {
        Material? material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Material not found");

        _context.Materials.Remove(material);
        return Ok();
    }
}