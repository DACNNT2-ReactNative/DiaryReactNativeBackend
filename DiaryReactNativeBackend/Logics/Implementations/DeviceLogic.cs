using AutoMapper;
using DiaryReactNativeBackend.AppExceptions;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Implementations;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Device;
using DiaryReactNativeBackend.RequestModels.Topic;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Implementations
{
    public class DeviceLogic : IDeviceLogic
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceLogic(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper; 
        }

        public async Task<string> DeleteDeviceForUser(string userId, string deviceToken)
        {
            var devices = await GetAllDevices();

            var deviceResponse = devices.FirstOrDefault(d => d.UserId == userId && d.DeviceToken == deviceToken);
            if (deviceResponse == null) throw new CustomException("Thiết bị không tồn tại");

            try
            {
                await _deviceRepository.Delete(deviceResponse);
                return deviceResponse.DeviceId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<DeviceModel>> GetAllDevices()
        {
            return await _deviceRepository.GetAll();
        }

        public async Task<DeviceModel> GetDeviceById(string deviceId)
        {
            return await _deviceRepository.GetById(deviceId);
        }

        public async Task<DeviceModel> GetDeviceByUserIdAndDeviceToken(string userId, string deviceToken)
        {
            var devices = await GetAllDevices();

            var deviceResponse = devices.FirstOrDefault(d => d.UserId == userId && d.DeviceToken == deviceToken);

            return deviceResponse;
        }

        public async Task<List<DeviceModel>> GetDeviceByUserId(string userId)
        {
            var devices = await GetAllDevices();

            var deviceResponse = devices.Where(d => d.UserId == userId ).ToList();

            return deviceResponse;
        }

        public async Task<string> SaveDevice(CreateDeviceRequestModel requestModel)
        {
            var device = _mapper.Map<CreateDeviceRequestModel, DeviceModel>(requestModel);

            device.DeviceId = Guid.NewGuid().ToString();

            try
            {
                var deviceId = await _deviceRepository.Save(device);
                return deviceId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
