using System.Runtime.CompilerServices;

namespace WebApplication1.Service.ImageService;

public interface ICloudImageService
{
    Task<bool> UploadFileAsync(IFormFile file, string? rootPath = "");
    Task<bool> DeleteFileAsync(string guid, string? rootPath = "");

    Task<byte[]?> DownloadFileAsync(string guid, string? rootPath = "");
}