using Amazon.DynamoDBv2.DataModel;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Implementations;

public class TopicRepository : HashKeyOnlyRepositoryBase<TopicModel, string>, ITopicRepository
{
    protected TopicRepository(IDynamoDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<string> SaveTopic(TopicModel topic)
    {
        await SaveAsync(topic);

        return topic.TopicId;
    }
}
