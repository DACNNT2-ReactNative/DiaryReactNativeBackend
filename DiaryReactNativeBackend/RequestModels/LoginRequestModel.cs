using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiaryReactNativeBackend.RequestModels;

public class LoginRequestModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public string PassCode { get; set; }
}
