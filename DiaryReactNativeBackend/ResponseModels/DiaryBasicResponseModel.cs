namespace DiaryReactNativeBackend.ResponseModels;

#nullable disable

public class DiaryBasicResponseModel
{
    public string DiaryId { get; set; }

    public string UserId { get; set; }

    public string TopicId { get; set; }

    public string Title { get; set; }

    public string Status { get; set; }

    public bool? isLiked { get; set; }

    public string Type { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }
}
