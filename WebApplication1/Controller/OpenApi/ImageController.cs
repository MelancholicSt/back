using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service.ImageService;

namespace WebApplication1.Controller;

[ApiController]
[Route("/marketplace-api/image")]
public class ImageController : ControllerBase
{
    private readonly ICloudImageService _imageService;
    private readonly DbContext _context;

    public ImageController(ICloudImageService imageService, DbContext context)
    {
        _imageService = imageService;
        _context = context;
    }

    [HttpGet("download")]
    public async Task<IActionResult> GetImage(string guid)
    {
        var content = await _imageService.DownloadFileAsync(guid);
        if (content == null)
            return NotFound("File not found");

        return File(content, "image/jpeg");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteImage(string guid)
    {
        if (await _imageService.DeleteFileAsync(guid))
            return Ok();

        return NotFound("File not found");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        if (await _imageService.UploadFileAsync(image))
            return Ok();

        if (!image.FileName.Contains(".jpg"))
            return BadRequest("Unsupported image file format sent. Supported formats: .jpg");

        return BadRequest("File already exists");
    }
}