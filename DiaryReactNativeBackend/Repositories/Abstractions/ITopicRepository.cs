using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Abstractions;

public interface ITopicRepository
{
    Task<string> SaveTopic(TopicModel topic);
}
