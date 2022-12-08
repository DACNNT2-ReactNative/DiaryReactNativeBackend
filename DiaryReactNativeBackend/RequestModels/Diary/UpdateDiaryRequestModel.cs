using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiaryReactNativeBackend.RequestModels.Diary;

public class UpdateDiaryRequestModel
{
    [Required]
    public string DiaryId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Status { get; set; }

    public string Type { get; set; }
}
