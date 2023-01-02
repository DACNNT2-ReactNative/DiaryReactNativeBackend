using System.ComponentModel.DataAnnotations;

namespace DiaryReactNativeBackend.RequestModels.User
{
    public class LoginGoogleRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
