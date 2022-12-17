using System.ComponentModel.DataAnnotations;
#nullable disable

namespace DiaryReactNativeBackend.RequestModels.Topic
{
    public class UpdateTopicRequestModel
    {
        [Required]
        public string TopicId { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}
