using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Topic;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Abstractions;

public interface ITopicLogic
{
    Task<TopicResponseModel> SaveTopic(CreateTopicRequestModel requestModel);
    Task<List<TopicResponseModel>> GetTopicsByUserId(string userId);
    Task<TopicResponseModel> UpdateTopic(UpdateTopicRequestModel requestModel);
    Task<List<TopicModel>> GetAllTopics();
    Task<string> DeleteTopicById(string topicId);
}
