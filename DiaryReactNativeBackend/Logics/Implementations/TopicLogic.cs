using AutoMapper;
using DiaryReactNativeBackend.AppExceptions;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Implementations;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Topic;
using DiaryReactNativeBackend.ResponseModels;
using Microsoft.IdentityModel.Tokens;

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

    public async Task<TopicResponseModel> SaveTopic(CreateTopicRequestModel requestModel)
    {
        var topic = _mapper.Map<CreateTopicRequestModel, TopicModel>(requestModel);

        topic.TopicId = Guid.NewGuid().ToString();
        topic.CreateAt = DateTime.Now;

        try
        {
            var topicId = await _topicRepository.SaveTopic(topic);
            var topicResponse = _mapper.Map<TopicModel, TopicResponseModel>(topic);
            return topicResponse;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<TopicResponseModel>> GetTopicsByUserId(string userId)
    {
        var topicsResponse = new List<TopicResponseModel>();
        var topics = await GetAllTopics();

        var topicsByUserId = topics.Where(t => t.UserId == userId).ToList();

        foreach(var topic in topicsByUserId)
        {
            var topicResponse = _mapper.Map<TopicModel, TopicResponseModel>(topic);
            topicsResponse.Add(topicResponse);
        }

        return topicsResponse;
    }

    public async Task<TopicResponseModel> UpdateTopic(UpdateTopicRequestModel requestModel)
    {
        var existingTopic = await _topicRepository.GetTopicById(requestModel.TopicId);
        if (existingTopic == null) throw new CustomException("Chủ đề không tồn tại");

        var topicUpdating = _mapper.Map<UpdateTopicRequestModel, TopicModel>(requestModel);
        topicUpdating.UserId = existingTopic.UserId;
        topicUpdating.CreateAt = existingTopic.CreateAt;
        topicUpdating.Name = requestModel.Name == null ? existingTopic.Name : requestModel.Name;
        topicUpdating.Image = requestModel.Image == null ? existingTopic.Image : requestModel.Image;

        try
        {
            var topicUpdatedId = await _topicRepository.SaveTopic(topicUpdating);
            var topicResponse = _mapper.Map<TopicModel, TopicResponseModel>(topicUpdating);
            return topicResponse;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }        
    }

    public async Task<List<TopicModel>> GetAllTopics()
    {
        return await _topicRepository.GetAllTopics();
    }

    public async Task<string> DeleteTopicById(string topicId)
    {
        var existingTopic = await _topicRepository.GetTopicById(topicId);
        if (existingTopic == null) throw new CustomException("Chủ đề không tồn tại");

        try
        {
            await _topicRepository.DeleteTopic(existingTopic);
            return topicId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
