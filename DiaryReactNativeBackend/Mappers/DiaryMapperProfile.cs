using AutoMapper;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Mappers;

public class DiaryMapperProfile : Profile
{
    public DiaryMapperProfile()
    {
        CreateMap<CreateDiaryRequestModel, DiaryModel>();
        CreateMap<UpdateDiaryRequestModel, DiaryModel>();

        CreateMap<DiaryModel, DiaryDetailResponseModel>();
        CreateMap<DiaryModel, DiaryBasicResponseModel>();
    }
}
