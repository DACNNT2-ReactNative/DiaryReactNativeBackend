using AutoMapper;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.ResponseModels;

namespace DiaryReactNativeBackend.Logics.Implementations;

public class DiaryLogic : IDiaryLogic
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IMapper _mapper;

    public DiaryLogic(IDiaryRepository diaryRepository, IMapper mapper)
    {
        _diaryRepository = diaryRepository;
        _mapper = mapper;
    }    

    public async Task<List<DiaryModel>> GetAllDiaries()
    {
        return await _diaryRepository.GetAllDiaries();
    }

    public async Task<List<DiaryBasicResponseModel>> GetDiariesByTopicId(string topicId)
    {
        var diariesResponse = new List<DiaryBasicResponseModel>();
        var diaries = await GetAllDiaries();
        var diraiesByUserIdAndTopicId = diaries.Where(d => d.TopicId == topicId).ToList();

        foreach (var diary in diraiesByUserIdAndTopicId)
        {
            var diaryResponse = _mapper.Map<DiaryModel, DiaryBasicResponseModel>(diary);
            diariesResponse.Add(diaryResponse);
        }

        return diariesResponse;
    }

    public async Task<DiaryDetailResponseModel> GetDiaryById(string diaryId)
    {
        var diary = await _diaryRepository.GetDiaryById(diaryId);
        var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);

        return diaryResponse;
    }

    public async Task<string> SaveDiary(CreateDiaryRequestModel requestModel)
    {
        var diary = _mapper.Map<CreateDiaryRequestModel, DiaryModel>(requestModel);
        diary.DiaryId = Guid.NewGuid().ToString();
        diary.CreateAt = DateTime.Now;
        diary.UpdateAt = DateTime.Now;

        try
        {
            var diaryId = await _diaryRepository.SaveDiary(diary);
            return diaryId;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string> UpdateDiary(UpdateDiaryRequestModel requestModel)
    {
        var existingDiary = await _diaryRepository.GetDiaryById(requestModel.DiaryId);
        if (existingDiary == null) throw new Exception("error");
        var diaryUpdating = _mapper.Map<UpdateDiaryRequestModel, DiaryModel>(requestModel);
        diaryUpdating.UserId = existingDiary.UserId;
        diaryUpdating.TopicId = existingDiary.TopicId;
        diaryUpdating.CreateAt = existingDiary.CreateAt;        
        diaryUpdating.Title = requestModel.Title == null ? existingDiary.Title : requestModel.Title;
        diaryUpdating.Content = requestModel.Content == null ? existingDiary.Content : requestModel.Content;
        diaryUpdating.Status = requestModel.Status == null ? existingDiary.Status : requestModel.Status;
        diaryUpdating.Type = requestModel.Type == null ? existingDiary.Type : requestModel.Type;
        diaryUpdating.UpdateAt = DateTime.Now;

        try
        {
            var diaryId = await _diaryRepository.SaveDiary(diaryUpdating);
            return diaryId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string> DeleteDiaryById(string diaryId)
    {
        var existingDiary = await _diaryRepository.GetDiaryById(diaryId);
        if (existingDiary == null) throw new Exception("error");
        try
        {
            await _diaryRepository.DeleteDiary(existingDiary);
            return diaryId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
