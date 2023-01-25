using System.ComponentModel.DataAnnotations;

namespace DiaryReactNativeBackend.RequestModels
{
    public class ForgotPasswordRequestModel
    {
        [Required]
        public string username { get; set; }
    }
}
