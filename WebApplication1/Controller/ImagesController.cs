using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service.ImageService;

namespace WebApplication1.Controller;

[ApiController]
public class ImagesController : ControllerBase
{
    private readonly ICloudImageService _imageService;

    public ImagesController(ICloudImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetImage(string guid)
    {
        var content = await _imageService.DownloadFileAsync(guid);
        if (content == null)
            return NotFound("File not found");
        
        return File(content, "application/octet-stream");
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
        if(await _imageService.UploadFileAsync(image))
            return Ok();
        
        return BadRequest("File already exists");
    }
}