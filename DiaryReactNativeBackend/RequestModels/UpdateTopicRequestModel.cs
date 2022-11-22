using System.ComponentModel.DataAnnotations;
#nullable disable

namespace DiaryReactNativeBackend.RequestModels
{
    public class UpdateTopicRequestModel
    {
        [Required]
        public string TopicId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
