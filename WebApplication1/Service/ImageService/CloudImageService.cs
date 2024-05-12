using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using WebApplication1.Data.dao;
using WebApplication1.Data.dao.Storage;

namespace WebApplication1.Service.ImageService;

public class CloudImageService : ICloudImageService
{
    private AmazonS3Client _client;
    private DbContext _context;

    public CloudImageService(DbContext context)
    {
        _context = context;
        BucketCredentials credentials = _context.BucketCredentials.First(x => x.Id == 1);
        var creds = new BasicAWSCredentials(credentials.KeyIdentifier, credentials.Key);
        var config = new AmazonS3Config()
        {
            ServiceURL = "https://s3.yandexcloud.net",
        };
        _client = new AmazonS3Client(creds, config);
    }

    public async Task<bool> UploadFileAsync(IFormFile file)
    {
        if (_context.Images.FirstOrDefault(x => x.FileName == file.FileName) != null)
            return false;

        Guid imgGuid = Guid.NewGuid();
        PutObjectRequest request = new PutObjectRequest
        {
            BucketName = "marketplace-builder",
            CannedACL = S3CannedACL.PublicRead,
            Key = "images/" + imgGuid + "-" + file.FileName
        };
        _context.Images.Add(new Image
        {
            Guid = imgGuid.ToString(),
            FileName = file.FileName
        });

        using (Stream inputStream = file.OpenReadStream())
        {
            request.InputStream = inputStream;
            await _client.PutObjectAsync(request);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFileAsync(string guid)
    {
        Image? image = _context.Images.FirstOrDefault(x => x.Guid == guid);
        if (image == null)
            return false;

        DeleteObjectRequest request = new DeleteObjectRequest
        {
            BucketName = "marketplace-builder",
            Key = "images/" + image.Guid + "-" + image.FileName
        };

        var response = await _client.DeleteObjectAsync(request);
        return true;
    }

    public async Task<byte[]?> DownloadFileAsync(string guid)
    {
        Image? image = _context.Images.FirstOrDefault(x => x.Guid == guid);
        if (image == null)
            return null;

        GetObjectRequest request = new GetObjectRequest
        {
            BucketName = "marketplace-builder",
            Key = "images/" + image.Guid + "-" + image.FileName
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
}