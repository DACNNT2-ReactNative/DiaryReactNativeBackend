using Amazon.S3;
using Amazon.S3.Model;
using DiaryReactNativeBackend.Services.Abstractions;
using DiaryReactNativeBackend.Services.Models;

namespace DiaryReactNativeBackend.Services.Implementations;

public class StorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;

    public StorageService(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<string> UploadFileAsync(S3ObjectUpload obj)
    {
        byte[] bytes = Convert.FromBase64String(obj.Base64String);

        var path = string.IsNullOrEmpty(obj.Prefix) ? obj.ImageName : $"{obj.Prefix?.TrimEnd('/')}/{obj.ImageName}";
        var bucketExists = await _s3Client.DoesS3BucketExistAsync(obj.BucketName);
        if (!bucketExists) throw new Exception("error");
        var request = new PutObjectRequest()
        {
            BucketName = obj.BucketName,
            Key = path,
            InputStream = new MemoryStream(bytes)
        };
        try
        {
            await _s3Client.PutObjectAsync(request);
            return $"https://{obj.BucketName}.s3.ap-southeast-1.amazonaws.com/{path}";
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }        
    }
}
