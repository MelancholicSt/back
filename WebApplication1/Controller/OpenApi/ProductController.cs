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
            MainImageGuid = material.MainImage.Guid,
            ImagesGuid = material.Images.Select(image => image.Guid),
            material.Measure,

        };

        return Ok(JsonConvert.SerializeObject(response));
    }
}