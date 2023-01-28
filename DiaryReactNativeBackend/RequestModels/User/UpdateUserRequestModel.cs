using System.ComponentModel.DataAnnotations;
#nullable disable

namespace DiaryReactNativeBackend.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        [Required]
        public string UserId { get; set; }
        public string PassCode { get; set; }

        public bool IsProtected { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
    }
}
