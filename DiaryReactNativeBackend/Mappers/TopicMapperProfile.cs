using AutoMapper;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels;

namespace DiaryReactNativeBackend.Mappers;

public class TopicMapperProfile : Profile
{
    public TopicMapperProfile()
    {
        CreateMap<CreateTopicRequestModel, TopicModel>();
    }
}
