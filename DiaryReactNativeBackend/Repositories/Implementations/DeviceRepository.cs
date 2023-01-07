using Amazon.DynamoDBv2.DataModel;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Implementations
{
    public class DeviceRepository: HashKeyOnlyRepositoryBase<DeviceModel, string>, IDeviceRepository
    {
        public DeviceRepository(IDynamoDBContext dbContext) : base(dbContext)
        {
        }

        public async Task Delete(DeviceModel device)
        {
            await DeleteAsync(device);
        }

        public async Task<List<DeviceModel>> GetAll()
        {
            return await GetList();
        }

        public async Task<DeviceModel> GetById(string deviceId)
        {
            return await GetByHashKey(deviceId);
        }

        public async Task<string> Save(DeviceModel device)
        {
            await SaveAsync(device);

            return device.DeviceId;
        }
    }
}
