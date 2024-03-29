﻿using AutoMapper;
using DiaryReactNativeBackend.AppExceptions;
using DiaryReactNativeBackend.Constant;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Repositories.Abstractions;
using DiaryReactNativeBackend.Repositories.Implementations;
using DiaryReactNativeBackend.Repositories.Models;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.ResponseModels;
using System.Text.RegularExpressions;
using System.Xml;

namespace DiaryReactNativeBackend.Logics.Implementations;

public class DiaryLogic : IDiaryLogic
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DiaryLogic(IDiaryRepository diaryRepository, IUserRepository userRepository, IMapper mapper)
    {
        _diaryRepository = diaryRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }    

    public async Task<List<DiaryModel>> GetAllDiaries()
    {
        return await _diaryRepository.GetAllDiaries();
    }

    public async Task<List<DiaryDetailResponseModel>> GetDiariesByTopicId(string topicId, string? searchKey)
    {
        var diariesResponse = new List<DiaryDetailResponseModel>();
        var diaries = await GetAllDiaries();

        if (searchKey == null || String.IsNullOrEmpty(searchKey)) {
            var diraiesByTopicId = diaries.Where(d => d.TopicId == topicId).ToList();
            foreach (var diary in diraiesByTopicId)
            {
                var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);
                diariesResponse.Add(diaryResponse);
            }
            return diariesResponse;
        }
        else
        {
            var diraiesByTopicId = diaries.Where(d => d.TopicId == topicId && Regex.Replace(d.Content, "<.*?>", " ").Contains(searchKey)).ToList();
            foreach (var diary in diraiesByTopicId)
            {
                var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);
                diariesResponse.Add(diaryResponse);
            }
            return diariesResponse;
        }
    }

    public async Task<DiaryDetailResponseModel> GetDiaryById(string diaryId)
    {
        var diary = await _diaryRepository.GetDiaryById(diaryId);
        var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);

        return diaryResponse;
    }

    public async Task<List<DiaryDetailResponseModel>> GetFavoriteDiariesByUserId(string userId, string? searchKey)
    {
        var diariesResponse = new List<DiaryDetailResponseModel>();
        var diaries = await GetAllDiaries();

        if (searchKey == null || String.IsNullOrEmpty(searchKey))
        {
            var favoriteDiraies = diaries.Where(d => d.UserId == userId && d.isLiked == true).ToList();
            foreach (var diary in favoriteDiraies)
            {
                var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);
                diariesResponse.Add(diaryResponse);
            }
            return diariesResponse;
        }
        else
        {
            var favoriteDiraies = diaries.Where(d => d.UserId == userId && d.isLiked == true && Regex.Replace(d.Content, "<.*?>", " ").Contains(searchKey)).ToList();
            foreach (var diary in favoriteDiraies)
            {
                var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);
                diariesResponse.Add(diaryResponse);
            }
            return diariesResponse;
        }
    }

    public async Task<List<DiaryDetailResponseModel>> GetSharedDiariesByUserId(string userId, string? searchKey)
    {
        var diariesResponse = new List<DiaryDetailResponseModel>();
        var diaries = await GetAllDiaries();

        if (searchKey == null || String.IsNullOrEmpty(searchKey))
        {
            var sharedDiraies = diaries.Where(d => d.UserId == userId && d.Status == Constants.DiaryStatus.PUBLIC).ToList();
            foreach (var diary in sharedDiraies)
            {
                var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);
                diariesResponse.Add(diaryResponse);
            }
            return diariesResponse;
        }
        else
        {
            var sharedDiraies = diaries.Where(d => d.UserId == userId && d.Status == Constants.DiaryStatus.PUBLIC && Regex.Replace(d.Content, "<.*?>", " ").Contains(searchKey)).ToList();
            foreach (var diary in sharedDiraies)
            {
                var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);
                diariesResponse.Add(diaryResponse);
            }
            return diariesResponse;
        }
    }

    public async Task<List<DiaryDetailResponseModel>> GetPublicDiaries()
    {
        var diariesResponse = new List<DiaryDetailResponseModel>();
        var diaries = await GetAllDiaries();
        var publicDiraies = diaries.Where(d => d.Status == Constants.DiaryStatus.PUBLIC).ToList();

        foreach (var diary in publicDiraies)
        {
            var diaryResponse = _mapper.Map<DiaryModel, DiaryDetailResponseModel>(diary);
            var user = await _userRepository.GetUserById(diaryResponse.UserId);
            diaryResponse.UserFullName = user.FullName;
            diariesResponse.Add(diaryResponse);
        }

        return diariesResponse;
    }

    public async Task<DiaryDetailResponseModel> SaveDiary(CreateDiaryRequestModel requestModel)
    {
        var diary = _mapper.Map<CreateDiaryRequestModel, DiaryModel>(requestModel);
        diary.DiaryId = Guid.NewGuid().ToString();
        diary.Status = Constants.DiaryStatus.PRIVATE;
        diary.CreateAt = DateTime.Now;
        diary.UpdateAt = DateTime.Now;

        try
        {
            var diaryId = await _diaryRepository.SaveDiary(diary);
            var diaryResult = await GetDiaryById(diaryId);
            return diaryResult;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<DiaryDetailResponseModel> UpdateDiary(UpdateDiaryRequestModel requestModel)
    {
        var existingDiary = await _diaryRepository.GetDiaryById(requestModel.DiaryId);
        if (existingDiary == null) throw new CustomException("Nhật ký không tồn tại");

        var diaryUpdating = _mapper.Map<UpdateDiaryRequestModel, DiaryModel>(requestModel);
        diaryUpdating.UserId = existingDiary.UserId;
        diaryUpdating.TopicId = existingDiary.TopicId;
        diaryUpdating.CreateAt = existingDiary.CreateAt;        
        diaryUpdating.Title = requestModel.Title == null ? existingDiary.Title : requestModel.Title;
        diaryUpdating.Content = requestModel.Content == null ? existingDiary.Content : requestModel.Content;
        diaryUpdating.Status = requestModel.Status == null ? existingDiary.Status : requestModel.Status;
        diaryUpdating.isLiked = requestModel.isLiked == null ? existingDiary.isLiked : requestModel.isLiked;
        diaryUpdating.Type = requestModel.Type == null ? existingDiary.Type : requestModel.Type;
        diaryUpdating.UpdateAt = DateTime.Now;

        try
        {
            var diaryId = await _diaryRepository.SaveDiary(diaryUpdating);
            var diaryResult = await GetDiaryById(diaryId);
            return diaryResult;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<string> DeleteDiaryById(string diaryId)
    {
        var existingDiary = await _diaryRepository.GetDiaryById(diaryId);
        if (existingDiary == null) throw new CustomException("Nhật ký không tồn tại");

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
