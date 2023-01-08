using System.ComponentModel.DataAnnotations;

namespace DiaryReactNativeBackend.RequestModels.Device
{
    public class DeleteDeviceRequestModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string DeviceToken { get; set; }

    }
}
