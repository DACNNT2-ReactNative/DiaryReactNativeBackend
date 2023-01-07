using Amazon.DynamoDBv2.DataModel;

namespace DiaryReactNativeBackend.Repositories.Models;

#nullable disable

[DynamoDBTable("users")]
public class DeviceModel
{
    [DynamoDBHashKey("deviceId")]
    public string DeviceId { get; set; }

    [DynamoDBProperty("userId")]
    public string UserId { get; set; }

    [DynamoDBProperty("deviceToken")]
    public string DeviceToken { get; set; }
}
