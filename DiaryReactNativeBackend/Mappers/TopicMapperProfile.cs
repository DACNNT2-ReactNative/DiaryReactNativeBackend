using AutoMapper;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Topic;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Mappers;

public class TopicMapperProfile : Profile
{
    public TopicMapperProfile()
    {
        CreateMap<CreateTopicRequestModel, TopicModel>();
        CreateMap<UpdateTopicRequestModel, TopicModel>();

        CreateMap<TopicModel, DiaryBasicResponseModel>();
        CreateMap<TopicModel, DiaryDetailResponseModel>();
    }
}
