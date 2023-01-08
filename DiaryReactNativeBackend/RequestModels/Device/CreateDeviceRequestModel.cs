using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiaryReactNativeBackend.RequestModels.Device;

public class CreateDeviceRequestModel
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string DeviceToken { get; set; }

    [Required]
    public string UserAgent { get; set; }
}
