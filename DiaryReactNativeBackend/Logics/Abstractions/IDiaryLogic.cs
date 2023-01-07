using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.RequestModels.Topic;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Abstractions;

public interface IDiaryLogic
{
    Task<DiaryDetailResponseModel> SaveDiary(CreateDiaryRequestModel requestModel);
    Task<List<DiaryDetailResponseModel>> GetDiariesByTopicId(string topicId, string? searchKey);
    Task<DiaryDetailResponseModel> GetDiaryById(string diaryId);
    Task<List<DiaryDetailResponseModel>> GetFavoriteDiariesByUserId(string userId, string? searchKey);
    Task<List<DiaryDetailResponseModel>> GetSharedDiariesByUserId(string userId, string? searchKey);
    Task<List<DiaryDetailResponseModel>> GetPublicDiaries();
    Task<DiaryDetailResponseModel> UpdateDiary(UpdateDiaryRequestModel requestModel);
    Task<List<DiaryModel>> GetAllDiaries();
    Task<string> DeleteDiaryById(string diaryId);
}
