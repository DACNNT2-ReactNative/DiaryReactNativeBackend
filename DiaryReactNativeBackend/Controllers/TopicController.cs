using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Logics.Implementations;
using DiaryReactNativeBackend.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace DiaryReactNativeBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ITopicLogic _topicLogic;
    public TopicController(IConfiguration configuration, ITopicLogic topicLogic)
    {
        _configuration = configuration;
        _topicLogic = topicLogic;
    }

    [HttpPost]
    [Route("create-topic")]
    public async Task<IActionResult> CreateTopic(CreateTopicRequestModel requestModel)
    {
        var topicsByUserId = await _topicLogic.GetTopicsByUserId(requestModel.UserId);
        var existedTopic = topicsByUserId.FirstOrDefault(x => x.Name == requestModel.Name);

        if(existedTopic != null)
        {
            return BadRequest("Topic name has already existed!");
        }

        try
        {
            var topicCreated = await _topicLogic.SaveTopic(requestModel);
            return Ok(topicCreated);
        }
        catch
        {
            return BadRequest("Can not create topic");
        }
    }

    [HttpPut]
    [Route("update-topic")]
    public async Task<IActionResult> UpdateTopic(UpdateTopicRequestModel requestModel)
    {
        var topics = await _topicLogic.GetAllTopics();
        var existedTopic = topics.FirstOrDefault(x => x.Name == requestModel.Name);

        if (existedTopic != null)
        {
            return BadRequest("Topic name has already existed!");
        }
        try
        {
            var topicUpdatedId = await _topicLogic.UpdateTopic(requestModel);
            return Ok(topicUpdatedId);
        }
        catch
        {
            return BadRequest("Can not update topic for now");
        }
    }

    [HttpGet]
    [Route("get-topics-by-user-id")]
    public async Task<IActionResult> GetTopicsByUserId(string userId)
    {
        var topics = await _topicLogic.GetTopicsByUserId(userId);

        return Ok(topics);
    }
}
