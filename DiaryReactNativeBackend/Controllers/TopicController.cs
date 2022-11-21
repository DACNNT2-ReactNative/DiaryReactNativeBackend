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
        var topicCreated = await _topicLogic.SaveTopic(requestModel);
        return Ok(topicCreated);
    }
}
