using AutoMapper;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Implementations;

public class TopicLogic : ITopicLogic
{
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;

    public TopicLogic(ITopicRepository topicRepository, IMapper mapper)
    {
        _topicRepository = topicRepository;
        _mapper = mapper;
    }

    public async Task<string> SaveTopic(CreateTopicRequestModel requestModel)
    {
        var topic = _mapper.Map<CreateTopicRequestModel, TopicModel>(requestModel);

        topic.TopicId = Guid.NewGuid().ToString();
        topic.CreateAt = DateTime.Now;

        var topicId = await _topicRepository.SaveTopic(topic);

        return topicId;
    }

    public async Task<List<TopicResponseModel>> GetTopicsByUserId(string userId)
    {
        var topicsResponse = new List<TopicResponseModel>();
        var topics = await _topicRepository.GetAllTopics();

        var topicsByUserId = topics.Where(t => t.UserId == userId).ToList();

        foreach(var topic in topicsByUserId)
        {
            var topicResponse = _mapper.Map<TopicModel, TopicResponseModel>(topic);
            topicsResponse.Add(topicResponse);
        }

        return topicsResponse;
    }

    public async Task<string> UpdateTopic(UpdateTopicRequestModel requestModel)
    {
        var existingTopic = await _topicRepository.GetTopicById(requestModel.TopicId);
        var topicUpdating = _mapper.Map<UpdateTopicRequestModel, TopicModel>(requestModel);
        topicUpdating.UserId = existingTopic.UserId;
        topicUpdating.CreateAt = existingTopic.CreateAt;

        var topicUpdatedId = await _topicRepository.SaveTopic(topicUpdating);

        return topicUpdatedId;
    }

    public async Task<List<TopicModel>> GetAllTopics()
    {
        return await _topicRepository.GetAllTopics();
    }
}
