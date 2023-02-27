using CorePush.Google;
using Microsoft.Extensions.Options;
using MtchMKRAPI.Data.Entities;
using static MtchMKRAPI.Data.Entities.GoogleNotification;
using System.Net.Http.Headers;
using CorePush.Apple;

namespace MtchMKRAPI.Services
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
       
    }

    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;

        public NotificationService(IOptions<FcmNotificationSetting> settings)
        {
            settings.Value.SenderId = "";
            settings.Value.ServerKey = "";
            _fcmNotificationSetting = settings.Value;
        }

        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            { 
                   
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = "636545007999", 
                        ServerKey = "AAAAlDUKAX8:APA91bGHQUh6RLUl-iRHZ4r03MAWrxMnsUX3UF2a7Z0uoihmiRDEPYqm6tw7cvm4CgQZKU-WdbSrfvUN51K-hWJ9K7s-9mLJSwMMF-qRaYqvA9H15wuFXt9_P0v-eup9od8DuVvqtwqX"  
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceToken;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
              
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

    //    public async Task<ApnsResponse> SendAppleNotification(NotificationModel notificationModel)
    //    {
    //        ApnsResponse apnsResponse = new ApnsResponse();
    //        var settings = new ApnSettings
    //        {
    //            //  AppBundleIdentifier = apnBundleId,
    //            P8PrivateKey = "89CKRAXCXR",
    //           // P8PrivateKeyId = "89CKRAXCXR"
    //            TeamId = "57H6TW6JBT",
    //            //    ServerType = apnServerType,
    //        };
    //        HttpClient httpClient = new HttpClient();

    //        string authorizationKey = string.Format("P8PrivateKey={0}", settings.P8PrivateKey);
    //        string deviceToken = notificationModel.DeviceToken;

    //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
    //        httpClient.DefaultRequestHeaders.Accept
    //                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
    //            var apn = new ApnSender(settings, httpClient);
               
    //            apnsResponse = await apn.SendAsync(notificationModel, deviceToken);

    //        return apnsResponse;
    //    }
    }
}
