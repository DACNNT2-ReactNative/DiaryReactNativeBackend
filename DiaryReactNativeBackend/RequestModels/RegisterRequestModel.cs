using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiaryReactNativeBackend.Repositories.Models;

public class RegisterRequestModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string FullName { get; set; }
}
