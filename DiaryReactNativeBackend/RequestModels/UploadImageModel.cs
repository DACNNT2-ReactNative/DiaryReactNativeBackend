using System.ComponentModel.DataAnnotations;

namespace DiaryReactNativeBackend.RequestModels;

public class UploadImageModel
{
    [Required]
    public string ImageName { get; set; }
    [Required]
    public string Base64String { get; set; }
}
