using Amazon.DynamoDBv2.DataModel;

#nullable disable

namespace DiaryReactNativeBackend.Repositories.Models;

[DynamoDBTable("users")]
public class UserModel
{
    [DynamoDBHashKey("userId")]
    public string UserId { get; set; }

    [DynamoDBProperty("username")]
    public string Username { get; set; }

    [DynamoDBProperty("password")]
    public string Password { get; set; }

    [DynamoDBProperty("fullName")]
    public string FullName { get; set; }

    [DynamoDBProperty("isProtected")]
    public Boolean IsProtected { get; set; }

    [DynamoDBProperty("passCode")]
    public string PassCode { get; set; }

    [DynamoDBProperty("email")]
    public string Email { get; set; }

    [DynamoDBProperty("typeLogin")]
    public string TypeLogin { get; set; }

    [DynamoDBProperty("createAt")]
    public DateTime CreateAt { get; set; }
}
