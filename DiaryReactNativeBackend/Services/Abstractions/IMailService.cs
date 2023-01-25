using AutoMapper.Internal;
using DiaryReactNativeBackend.RequestModels;

namespace DiaryReactNativeBackend.Services.Abstractions
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
