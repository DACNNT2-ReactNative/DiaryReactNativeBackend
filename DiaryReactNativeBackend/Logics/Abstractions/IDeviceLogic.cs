using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Device;
using DiaryReactNativeBackend.RequestModels.User;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Abstractions
{
    public interface IDeviceLogic
    {
        Task<string> SaveDevice(CreateDeviceRequestModel requestModel);
        Task<IEnumerable<DeviceModel>> GetAllDevices();
        Task<DeviceModel> GetDeviceByUserIdAndDeviceToken(string userId, string deviceToken);
        Task<List<DeviceModel>> GetDeviceByUserId(string userId);
        Task<DeviceModel> GetDeviceById(string deviceId);
        Task<string> DeleteDeviceForUser(string userId, string deviceToken);
    }
}
