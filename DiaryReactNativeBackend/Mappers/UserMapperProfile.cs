using AutoMapper;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.User;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterRequestModel, UserModel>();
        CreateMap<UserModel, UserResponseModel>();
        CreateMap<LoginGoogleRequestModel, UserModel>();
    }
}
