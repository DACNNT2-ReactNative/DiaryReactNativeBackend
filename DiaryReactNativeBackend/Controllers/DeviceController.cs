using CorePush.Google;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Logics.Implementations;
using DiaryReactNativeBackend.Model;
using DiaryReactNativeBackend.RequestModels.Device;
using DiaryReactNativeBackend.RequestModels.Diary;
using DiaryReactNativeBackend.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using static DiaryReactNativeBackend.Model.GoogleNotification;

namespace DiaryReactNativeBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IDeviceLogic _deviceLogic;
    private readonly FcmNotificationSetting _fcmNotificationSetting;

    public DeviceController(IConfiguration configuration, IDeviceLogic deviceLogic, IOptions<FcmNotificationSetting> settings)
    {
        _configuration = configuration;
        _deviceLogic = deviceLogic;
        _fcmNotificationSetting = settings.Value;
    }

    [Authorize]
    [HttpPost]
    [Route("create-device")]
    public async Task<IActionResult> CreateDevice(CreateDeviceRequestModel requestModel)
    {
        try
        {
            var checkDeviceForUser = await _deviceLogic.GetDeviceByUserId(requestModel.UserId);
            var existingDeviceToken = await _deviceLogic.GetDeviceByUserIdAndDeviceToken(requestModel.UserId, requestModel.DeviceToken);
            
            if(existingDeviceToken != null)
            {
                return Ok("Success");
            }
            
            if (existingDeviceToken == null && checkDeviceForUser.Count == 0)
            {
                var deviceCreated = await _deviceLogic.SaveDevice(requestModel);

                return Ok(deviceCreated);
            }

            if (checkDeviceForUser != null && checkDeviceForUser.Count > 0)
            {
                try
                {
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };

                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = checkDeviceForUser[0].DeviceToken;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = "Thông báo";
                    dataPayload.Body = "Tài khoản của bạn đang được đăng nhập tại thiết bị " + requestModel.UserAgent;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);
                    var deviceCreated = await _deviceLogic.SaveDevice(requestModel);

                    return Ok(deviceCreated);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        
            return Ok("Success");
        }
        catch
        {
            return BadRequest("Tạo thiết bị không thành công.");
        }
    }

    [Authorize]
    [HttpPost]
    [Route("delete-device")]
    public async Task<IActionResult> DeleteDevice(DeleteDeviceRequestModel requestModel)
    {
        {
            var deviceCreated = await _deviceLogic.DeleteDeviceForUser(requestModel.UserId, requestModel.DeviceToken);
            return Ok(deviceCreated);
        }
    }
}
