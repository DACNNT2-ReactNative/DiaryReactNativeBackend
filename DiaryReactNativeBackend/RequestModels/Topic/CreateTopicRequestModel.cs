using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiaryReactNativeBackend.RequestModels.Topic;

public class CreateTopicRequestModel
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Name { get; set; }
}
