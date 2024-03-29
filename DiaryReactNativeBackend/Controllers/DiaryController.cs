﻿using DiaryReactNativeBackend.AppExceptions;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Logics.Implementations;
using DiaryReactNativeBackend.RequestModels.Diary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DiaryReactNativeBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiaryController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IDiaryLogic _diaryLogic;

    public DiaryController(IConfiguration configuration, IDiaryLogic diaryLogic)
    {
        _configuration = configuration;
        _diaryLogic = diaryLogic;
    }

    [HttpPost]
    [Route("create-diary")]
    public async Task<IActionResult> CreateDiary(CreateDiaryRequestModel requestModel)
    {
        try
        {
            var diaryCreated = await _diaryLogic.SaveDiary(requestModel);
            return Ok(diaryCreated);
        }
        catch
        {
            return BadRequest("Tạo nhật ký không thành công.");
        }
    }

    [HttpPut]
    [Route("update-diary")]
    public async Task<IActionResult> UpdateDiary(UpdateDiaryRequestModel requestModel)
    {
        try
        {
            var diaryUpdated = await _diaryLogic.UpdateDiary(requestModel);
            return Ok(diaryUpdated);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest("Lưu nhật ký không thành công.");
        }
    }

    [HttpGet]
    [Route("get-diaries-by-topic-id")]
    public async Task<IActionResult> GetDiariesByTopicId(string topicId, string? searchKey)
    {
        var diaries = await _diaryLogic.GetDiariesByTopicId(topicId, searchKey);

        foreach (var diary in diaries)
        {
            if (diary.Content.IsNullOrEmpty())
            {
                try
                {
                    await _diaryLogic.DeleteDiaryById(diary.DiaryId);
                }
                catch { }
            }
        }

        var results = diaries.Where(d => d.Content.IsNullOrEmpty() == false).ToList();

        return Ok(results);
    }

    [HttpGet]
    [Route("get-diary-by-id")]
    public async Task<IActionResult> GetDiaryById(string diaryId)
    {
        try
        {
            var diary = await _diaryLogic.GetDiaryById(diaryId);
            if (diary == null)
            {
                return BadRequest("Nhật ký không tồn tại");
            }
            return Ok(diary);
        }
        catch
        {
            return BadRequest("Không tìm thấy nhật ký");
        }
    }

    [HttpGet]
    [Route("get-favorite-diaries-by-user-id")]
    public async Task<IActionResult> GetFavoriteDiariesByUserId(string userId, string? searchKey)
    {
        var diaries = await _diaryLogic.GetFavoriteDiariesByUserId(userId, searchKey);

        return Ok(diaries);
    }

    [HttpGet]
    [Route("get-shared-diaries-by-user-id")]
    public async Task<IActionResult> GetSharedDiariesByUserId(string userId, string? searchKey)
    {
        var diaries = await _diaryLogic.GetSharedDiariesByUserId(userId, searchKey);

        return Ok(diaries);
    }

    [HttpGet]
    [Route("get-public-diaries")]
    public async Task<IActionResult> GetPublicDiaries()
    {
        var diaries = await _diaryLogic.GetPublicDiaries();

        return Ok(diaries);
    }

    [HttpDelete]
    [Route("delete-diary-by-id")]
    public async Task<IActionResult> DeleteDiaryById(string diaryId)
    {
        try
        {
            var diaryDeleted = await _diaryLogic.DeleteDiaryById(diaryId);
            return Ok(diaryDeleted);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest("Xóa nhật ký không thành công");
        }
    }
}
