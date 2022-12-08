using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Abstractions;

public interface IDiaryRepository
{
    Task<List<DiaryModel>> GetAllDiaries();
    Task<string> SaveDiary(DiaryModel diary);
    Task<DiaryModel> GetDiaryById(string diaryId);

    Task DeleteDiary(DiaryModel diary);
}
