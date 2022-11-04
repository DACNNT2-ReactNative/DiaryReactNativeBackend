using System.ComponentModel.DataAnnotations;

namespace DiaryReactNativeBackend.RequestModels;

public class UploadImageModel
{
    [Required]
    public IFormFile Image { get; set; }
}
