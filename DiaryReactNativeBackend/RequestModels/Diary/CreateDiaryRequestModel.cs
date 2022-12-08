using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiaryReactNativeBackend.RequestModels.Diary;

public class CreateDiaryRequestModel
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string TopicId { get; set; }

    public string Type { get; set; }
}
