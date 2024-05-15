using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using WebApplication1.Data.dao;

namespace WebApplication1.Service.ImageService;

public class CloudImageService : ICloudImageService
{
    private AmazonS3Client _client;
    private DbContext _context;

    public CloudImageService(DbContext context)
    {
        _context = context;
        BucketCredentials? credentials = _context.BucketCredentials.FirstOrDefault(x => x.Id == 1);
        if (credentials == null)
            throw new DataException("Credentials not found");
        var creds = new BasicAWSCredentials(credentials.KeyIdentifier, credentials.Key);
        var config = new AmazonS3Config()
        {
            ServiceURL = "https://s3.yandexcloud.net",
        };
        _client = new AmazonS3Client(creds, config);
    }

    public async Task<bool> UploadFileAsync(IFormFile file, string? rootPath)
    {
        Guid imgGuid = Guid.NewGuid();
        string extensionName = Path.GetExtension(file.FileName);
        var imgPath = await GetFormattedImagePathAsync(imgGuid.ToString(), extensionName);

        PutObjectRequest request = new PutObjectRequest
        {
            BucketName = "marketplace-builder",
            CannedACL = S3CannedACL.PublicRead,
            Key = $"{rootPath}/{imgPath}"
        };
        _context.Images.Add(new Image
        {
            Guid = imgGuid.ToString(),
            Extension = extensionName
        });

        using (Stream inputStream = file.OpenReadStream())
        {
            request.InputStream = inputStream;
            await _client.PutObjectAsync(request);
        }

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteFileAsync(string guid, string? rootPath)
    {
        Image? image = _context.Images.FirstOrDefault(x => x.Guid == guid);
        if (image == null)
            return false;
        var imgPath = await GetFormattedImagePathAsync(guid, image.Extension);
        DeleteObjectRequest request = new DeleteObjectRequest
        {
            BucketName = "marketplace-builder",
            Key = $"{rootPath}/{imgPath}"
        };
        await _client.DeleteObjectAsync(request);
        return true;
    }
    

    public async Task<byte[]?> DownloadFileAsync(string guid, string? rootPath)
    {
        Image? image = _context.Images.FirstOrDefault(x => x.Guid == guid);
        if (image == null)
            return null;

        GetObjectRequest request = new GetObjectRequest
        {
            BucketName = "marketplace-builder",
            Key = $"{rootPath}/{await GetFormattedImagePathAsync(guid, image.Extension)}"
        };
        using var response = await _client.GetObjectAsync(request);
        using MemoryStream ms = new MemoryStream();
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            await response.ResponseStream.CopyToAsync(ms);
            return ms.ToArray();
        }

        throw new BadHttpRequestException("On download file");
    }

    private async Task<string> GetFormattedImagePathAsync(string guid, string imageExtension)
    {
        var folderName = await GetImageFolderNameAsync(guid);
        var imageName = GetImageName(guid);
        return $"{folderName}/{imageName}.{imageExtension}";
    }

    private async Task<string> GetImageFolderNameAsync(string guid)
    {
        using MD5 md5 = MD5.Create();
        using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(guid));
        return Convert.ToHexString(await md5.ComputeHashAsync(stream));
    }

    private string GetImageName(string guid)
    {
        return guid.Substring(guid.Length - 6);
    }
}