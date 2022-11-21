using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Abstractions;

public interface IUserRepository
{
    Task<string> SaveUser(UserModel user);
    Task<List<UserModel>> GetAllUsers();
    Task<UserModel> GetUserById(string userId);
}
