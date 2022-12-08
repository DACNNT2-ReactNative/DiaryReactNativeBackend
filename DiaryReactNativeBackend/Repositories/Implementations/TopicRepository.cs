using Amazon.DynamoDBv2.DataModel;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Implementations;

public class TopicRepository : HashKeyOnlyRepositoryBase<TopicModel, string>, ITopicRepository
{
    public TopicRepository(IDynamoDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<string> SaveTopic(TopicModel topic)
    {
        await SaveAsync(topic);

        return topic.TopicId;
    }

    public async Task<List<TopicModel>> GetAllTopics()
    {
        return await GetList();
    }

    public async Task<TopicModel> GetTopicById(string topicId)
    {
        return await GetByHashKey(topicId);
    }

    public async Task DeleteTopic(TopicModel topic)
    {
        await DeleteAsync(topic);
    }
}
