using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Abstractions;

public interface ITopicLogic
{
    Task<string> SaveTopic(CreateTopicRequestModel requestModel);
    Task<List<TopicResponseModel>> GetTopicsByUserId(string userId);
    Task<string> UpdateTopic(UpdateTopicRequestModel requestModel);
    Task<List<TopicModel>> GetAllTopics();
}
