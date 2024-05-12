namespace WebApplication1.Service.ImageService;

public interface ICloudImageService
{
    Task<bool> UploadFileAsync(IFormFile file);
    Task<bool> DeleteFileAsync(string guid);
    Task<byte[]?> DownloadFileAsync(string guid);
}