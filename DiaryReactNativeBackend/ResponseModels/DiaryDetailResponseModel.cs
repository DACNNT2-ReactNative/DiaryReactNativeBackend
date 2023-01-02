namespace DiaryReactNativeBackend.ResponseModels;

#nullable disable

public class DiaryDetailResponseModel : DiaryBasicResponseModel
{
    public string Content { get; set; }
    public string UserFullName { get; set; }
}
