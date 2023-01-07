using Amazon.DynamoDBv2.DataModel;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;

namespace DiaryReactNativeBackend.Repositories.Implementations;

public class DiaryRepository : HashKeyOnlyRepositoryBase<DiaryModel, string>, IDiaryRepository
{
    public DiaryRepository(IDynamoDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<DiaryModel>> GetAllDiaries()
    {
        return await GetList();
    }

    public async Task<DiaryModel> GetDiaryById(string diaryId)
    {
        return await GetByHashKey(diaryId);
    }

    public async Task<string> SaveDiary(DiaryModel diary)
    {
        await SaveAsync(diary);

        return diary.DiaryId;
    }

    public async Task DeleteDiary(DiaryModel diary)
    {
        await DeleteAsync(diary);
    }
}
