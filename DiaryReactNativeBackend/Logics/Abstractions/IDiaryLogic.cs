using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.RequestModels.Topic;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Abstractions;

public interface IDiaryLogic
{
    Task<string> SaveDiary(CreateDiaryRequestModel requestModel);
    Task<List<DiaryDetailResponseModel>> GetDiariesByTopicId(string topicId);
    Task<DiaryDetailResponseModel> GetDiaryById(string diaryId);
    Task<string> UpdateDiary(UpdateDiaryRequestModel requestModel);
    Task<List<DiaryModel>> GetAllDiaries();
    Task<string> DeleteDiaryById(string diaryId);
}
