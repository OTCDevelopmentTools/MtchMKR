﻿using Newtonsoft.Json;

namespace MtchMKRAPI.Data.Entities
{
    public class NotificationModel
    {
        [JsonProperty("DeviceToken")]
        public string DeviceToken { get; set; }
        [JsonProperty("isAndroiodDevice")]
        public bool IsAndroiodDevice { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class GoogleNotification
    {
        public class DataPayload
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }
        }
        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";
        [JsonProperty("data")]
        public DataPayload Data { get; set; }
        [JsonProperty("notification")]
        public DataPayload Notification { get; set; }
    }

    public class AppleNotification
    {
        public class ApsPayload
        {
            public class Alert
            {
                [JsonProperty("title")]
                public string Title { get; set; }

                [JsonProperty("body")]
                public string Body { get; set; }
            }

            [JsonProperty("alert")]
            public Alert AlertBody { get; set; }

            [JsonProperty("apns-push-type")]
            public string PushType { get; set; } = "alert";
        }

        public AppleNotification(Guid id, string message, string title = "")
        {
            Id = id;

            Aps = new ApsPayload
            {
                AlertBody = new ApsPayload.Alert
                {
                    Title = title,
                    Body = message
                }
            };
        }

        [JsonProperty("aps")]
        public ApsPayload Aps { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
