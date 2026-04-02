using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SAP_BTS_API.Domain.common
{
    public class BaseResponse<TResponse>
    {
        [JsonPropertyName("isSuccess")]
        public bool isSuccess { get; set; }

        [JsonPropertyName("message")]
        public string message { get; set; } = string.Empty;

        [JsonPropertyName("statusCode")]
        public HttpStatusCode statusCode { get; set; }

        [JsonPropertyName("Data")]
        public TResponse Data { get; set; }
        public static BaseResponse<TResponse> Failure(HttpStatusCode statusCode = HttpStatusCode.BadRequest, string message = "Create Failed!!", TResponse? data = default(TResponse))
        {
            return new BaseResponse<TResponse>()
            {
                isSuccess = false,
                statusCode = statusCode,
                message = message,
                Data = data 
             
            };
        }
        public static BaseResponse<TResponse> Ok(TResponse? data = default(TResponse),HttpStatusCode statusCode = HttpStatusCode.OK,string message = "Success")
        {
            return new BaseResponse<TResponse>()
            {
                isSuccess = true,
                statusCode = statusCode,
                Data = data,
                message = message
            };
        }
    }
}
