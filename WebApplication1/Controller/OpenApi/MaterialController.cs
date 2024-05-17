using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao.Product.Details;

namespace WebApplication1.Controller.OpenApi;

[ApiController]
[Route("api/materials/")]
public class MaterialController : ControllerBase
{
    private DbContext _context;

    public MaterialController(DbContext context)
    {
        _context = context;
    }

    [HttpGet("{materialId}/suppliers")]
    public async Task<IActionResult> GetMaterialSuppliers(ulong materialId)
    {
        Material? material = await _context.Materials.Include(material => material.Suppliers)
            .ThenInclude(supplier => supplier.AvailableMaterials)
            .FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Material not found");

        var suppliers = material.Suppliers.Where(supplier => supplier.AvailableMaterials.Contains(material));
        var ids = suppliers.Select(supplier => supplier.Id).ToList();

        return Ok(JsonConvert.SerializeObject(ids));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllMaterials()
    {
        var materials = await _context.Materials.ToListAsync();
        var ids = materials.Select(material => material.Id);
        return Ok(JsonConvert.SerializeObject(ids));
    }

    [HttpGet("{materialId}")]
    public async Task<IActionResult> GetMaterialById(ulong materialId)
    {
        var material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Material not found");

        return Ok(JsonConvert.SerializeObject(material));
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMaterial(string materialName, string measureName)
    {
        bool isMaterialExistsAlready =
            _context.Materials.Select(material => material.Name.ToLower()).Contains(materialName);
        if (isMaterialExistsAlready)
            return BadRequest("Material already exists");
        Material material = new Material
        {
            Name = materialName,
            Measure = new Measure
            {
                MeasureName = measureName
            }
        };

        await _context.Materials.AddAsync(material);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{materialId}/replace")]
    public async Task<IActionResult> ReplaceMaterial(ulong materialId, string newName, string? newMeasureName)
    {
        Material? material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Material not found");
        if (material.Name.ToLower() == newName.ToLower())
            return BadRequest("Names equals");

        material.Name = newName;
        if (newMeasureName != null)
        {
            Measure newMeasure = new Measure
            {
                MeasureName = newMeasureName
            };
            material.Measure = newMeasure;
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPatch("{materialId}/edit/name")]
    public async Task<IActionResult> EditMaterialName(ulong materialId, string newName)
    {
        Material? material = await _context.Materials.FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Material not found");

        material.Name = newName;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("{materialId}/edit/measure")]
    public async Task<IActionResult> EditMeasureName(ulong materialId, string measureName)
    {
        Material? material = await _context.Materials.Include(material => material.Measure)
            .FirstOrDefaultAsync(material => material.Id == materialId);
        if (material == null)
            return NotFound("Material not found");

        material.Measure.MeasureName = measureName;
        await _context.SaveChangesAsync();
        return Ok();
    }
}