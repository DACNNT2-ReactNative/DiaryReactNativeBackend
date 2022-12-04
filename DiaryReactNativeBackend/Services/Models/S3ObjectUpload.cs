namespace DiaryReactNativeBackend.Services.Models;    

public class S3ObjectUpload
{
    public string ImageName { get; set; }
    public string Base64String { get; set; }   
    public string BucketName { get; set; } = null!;
    public string? Prefix { get; set; } = null!;

    public S3ObjectUpload(string imageName, string base64String, string bucketName, string? prefix)
    {
        ImageName = imageName;
        Base64String = base64String;
        BucketName = bucketName;
        Prefix = prefix;
    }
}
