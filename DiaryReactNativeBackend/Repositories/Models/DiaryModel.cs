using Amazon.DynamoDBv2.DataModel;

#nullable disable

namespace DiaryReactNativeBackend.Repositories.Models;

[DynamoDBTable("diaries")]
public class DiaryModel
{
    [DynamoDBHashKey("diaryId")]
    public string DiaryId { get; set; }

    [DynamoDBProperty("userId")]
    public string UserId { get; set; }

    [DynamoDBProperty("topicId")]
    public string TopicId { get; set; }

    [DynamoDBProperty("title")]
    public string Title { get; set; }

    [DynamoDBProperty("content")]
    public string Content { get; set; }

    [DynamoDBProperty("status")]
    public string Status { get; set; }

    [DynamoDBProperty("type")]
    public string Type { get; set; }

    [DynamoDBProperty("createAt")]
    public DateTime CreateAt { get; set; }

    [DynamoDBProperty("updateAt")]
    public DateTime UpdateAt { get; set; }
}
