using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Repositories.Abstractions;

public interface ITopicRepository
{
    Task<List<TopicModel>> GetAllTopics();
    Task<string> SaveTopic(TopicModel topic);
    Task<TopicModel> GetTopicById(string topicId);
    Task DeleteTopic(TopicModel topic);
}
