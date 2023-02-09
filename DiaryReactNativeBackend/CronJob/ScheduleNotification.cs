using CorePush.Google;
using DiaryReactNativeBackend.Logics.Abstractions;
using DiaryReactNativeBackend.Model;
using Quartz;
using static DiaryReactNativeBackend.Model.GoogleNotification;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace DiaryReactNativeBackend.CronJob
{


    public class ScheduleNotification : IJob
    {
        private readonly IDeviceLogic _deviceLogic;
        private readonly FcmNotificationSetting _fcmNotificationSetting;

        public ScheduleNotification(IDeviceLogic deviceLogic, IOptions<FcmNotificationSetting> settings)
        {
            _deviceLogic = deviceLogic;
            _fcmNotificationSetting = settings.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var deviceTokens = await _deviceLogic.GetAllDevices();
            var listDevice = deviceTokens.ToList();
            FcmSettings settings = new FcmSettings()
            {
                SenderId = _fcmNotificationSetting.SenderId,
                ServerKey = _fcmNotificationSetting.ServerKey
            };

            HttpClient httpClient = new HttpClient();

            string authorizationKey = string.Format("keyy={0}", settings.ServerKey);

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
            httpClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            for (var i= 0; i < listDevice.Count(); i++)
            {
                try
                {
                    string deviceToken = listDevice[i].DeviceToken;
                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = "Thông báo";
                    dataPayload.Body = "Alo bạn ơi!! Đã đến lúc để ghi chú những việc cần làm rồi!!";

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
