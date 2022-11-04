using AutoMapper;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterRequestModel, UserModel>();
        CreateMap<UserModel, UserResponseModel>();
    }
}
