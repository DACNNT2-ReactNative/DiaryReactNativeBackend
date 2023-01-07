using AutoMapper;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Device;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Mappers
{
    public class DeviceMapperProfile : Profile
    {
        public DeviceMapperProfile()
        {
            CreateMap<CreateDeviceRequestModel, DeviceModel>();
        }
    }
}
