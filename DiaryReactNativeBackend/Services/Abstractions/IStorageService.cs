using DiaryReactNativeBackend.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaryReactNativeBackend.Services.Abstractions;

public interface IStorageService
{
    Task<string> UploadFileAsync(S3ObjectUpload obj);
}
