using Amazon.DynamoDBv2.DataModel;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Implementations;

public class UserRepository : HashKeyOnlyRepositoryBase<UserModel, string>, IUserRepository
{
    public UserRepository(IDynamoDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<string> SaveUser(UserModel user)
    {        
        await SaveAsync(user);

        return user.UserId;
    }

    public async Task<List<UserModel>> GetAllUsers()
    {
        return await GetList();
    }

    public async Task<UserModel> GetUserById(string userId)
    {
        return await GetByHashKey(userId);
    }
}
