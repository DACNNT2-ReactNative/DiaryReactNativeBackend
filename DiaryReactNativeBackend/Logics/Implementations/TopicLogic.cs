using AutoMapper;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels;

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
}
