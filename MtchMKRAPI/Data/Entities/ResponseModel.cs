using Newtonsoft.Json;

namespace MtchMKRAPI.Data.Entities
{
    public class ResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
