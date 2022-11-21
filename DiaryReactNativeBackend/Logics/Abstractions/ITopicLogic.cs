using DiaryReactNativeBackend.RequestModels;

namespace DiaryReactNativeBackend.Logics.Abstractions;

public interface ITopicLogic
{
    Task<string> SaveTopic(CreateTopicRequestModel requestModel);
}
