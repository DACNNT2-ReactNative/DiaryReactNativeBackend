namespace DiaryReactNativeBackend.ResponseModels;

#nullable disable

public class UserResponseModel
{
    public string UserId { get; set; }

    public string FullName { get; set; }

    public Boolean IsProtected { get; set; }

    public string Email { get; set; }    
}
