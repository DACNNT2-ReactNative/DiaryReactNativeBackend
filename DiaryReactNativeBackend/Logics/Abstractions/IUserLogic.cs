using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Abstractions;

public interface IUserLogic
{
    Task<string> SaveUser(RegisterRequestModel requestModel);
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<List<UserResponseModel>> GetUsersPortal();
    Task<UserResponseModel> GetUserById(string userId);
}
