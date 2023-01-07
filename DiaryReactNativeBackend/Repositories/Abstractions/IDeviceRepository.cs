using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Abstractions
{
    public interface IDeviceRepository
    {
        Task<List<DeviceModel>> GetAll();
        Task<string> Save(DeviceModel device);
        Task<DeviceModel> GetById(string deviceId);
        Task Delete(DeviceModel device);
    }
}
