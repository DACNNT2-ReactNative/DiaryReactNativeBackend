using Amazon.DynamoDBv2.DataModel;

#nullable disable

namespace DiaryReactNativeBackend.Repositories.Models;

[DynamoDBTable("topics")]
public class TopicModel
{
    [DynamoDBHashKey("topicId")]
    public string TopicId { get; set; }

    [DynamoDBProperty("userId")]
    public string UserId { get; set; }

    [DynamoDBProperty("Name")]
    public string Name { get; set; }

    [DynamoDBProperty("createAt")]
    public DateTime CreateAt { get; set; }
}
