using AlMohamyProject.Dtos;
using BL.Dtos;
using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Threading;
using BL.Helpers;
using CorePush.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;

namespace BL
{
    public interface INotificationServiceqq
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }

    public interface IFcmSenderr
    {
        Task<FcmResponse> SendAsync(string deviceId, object payload, CancellationToken cancellationToken = default(CancellationToken));

        Task<FcmResponse> SendAsync(object payload, CancellationToken cancellationToken = default(CancellationToken));
    }
    public class ClsNotification1 : INotificationServiceqq
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;

        public ClsNotification1(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
        }

        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                //FcmSettings settings = new FcmSettings()
                //{
                //    SenderId = "296449306490",
                //    ServerKey = "AAAARQXBb3o:APA91bE8c59nrm6doGaXDZ_1teBbEVHVi6KJ2N9_Ahu7lc0jGf6cgSVS_8f8SRYF-5A-6kPUr1BDIvJyqQZ5-UbEeboaYAM_0ISvoTd6uxEzWaifUNqy02X47gasTTl-1eC8q3k0hvQZ"
                //};

                FcmSettings settings = new FcmSettings()
                {
                    SenderId = _fcmNotificationSetting.SenderId,
                    ServerKey = _fcmNotificationSetting.ServerKey
                };

                HttpClient httpClient = new HttpClient();
                string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                string deviceToken = notificationModel.DeviceId;
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                httpClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                GoogleNotification.DataPayload dataPayload = new GoogleNotification.DataPayload();
                dataPayload.Title = notificationModel.Title;
                dataPayload.Body = notificationModel.Body;
                GoogleNotification notification = new GoogleNotification();
                notification.Data = dataPayload;
                //notification.Notification = dataPayload;
               

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
                response.Message = $"Something went wrong : {ex.Message}";
                return response;
            }
        }
    }

    public class FcmSettings
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
    }
    public class FcmSender : IFcmSenderr
    {
        private const string fcmUrl = "https://fcm.googleapis.com/fcm/send";

        private readonly FcmSettings settings;

        private readonly HttpClient http;

        public FcmSender(FcmSettings settings, HttpClient http)
        {
            this.settings = settings ?? throw new ArgumentNullException("settings");
            this.http = http ?? throw new ArgumentNullException("http");
            if (http.BaseAddress == null)
            {
                http.BaseAddress = new Uri("https://fcm.googleapis.com/fcm/send");
            }
        }

        public Task<FcmResponse> SendAsync(string deviceId, object payload, CancellationToken cancellationToken = default(CancellationToken))
        {
            JObject jObject = JObject.FromObject(payload);
            jObject.Remove("to");
            jObject.Add("to", JToken.FromObject(deviceId));
            return SendAsync(jObject, cancellationToken);
        }

        public async Task<FcmResponse> SendAsync(object payload, CancellationToken cancellationToken = default(CancellationToken))
        {
            string content = JsonHelper.Serialize(payload);
            using HttpRequestMessage message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.Headers.Add("Authorization", "key = " + settings.ServerKey);
            if (!string.IsNullOrEmpty(settings.SenderId))
            {
                message.Headers.Add("Sender", "id = " + settings.SenderId);
            }

            message.Content = new StringContent(content, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await http.SendAsync(message, cancellationToken);
            string text = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Firebase notification error: " + text);
            }

            return JsonHelper.Deserialize<FcmResponse>(text);
        }
       

    }

    public class FcmResponse
    {
        [JsonProperty("multicast_id")]
        public string MulticastId { get; set; }

        [JsonProperty("canonical_ids")]
        public int CanonicalIds { get; set; }

        public int Success { get; set; }

        public int Failure { get; set; }

        public List<FcmResult> Results { get; set; }

        public bool IsSuccess()
        {
            if (Success > 0)
            {
                return Failure == 0;
            }

            return false;
        }
    }
    public class FcmResult
    {
        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("registration_id")]
        public string RegistrationId { get; set; }

        public string Error { get; set; }
    }
}
