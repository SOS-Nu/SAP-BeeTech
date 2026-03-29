using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP_DIAPI_Demo.Models
{
    public class BaseResponse<TResponse>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public TResponse Data { get; set; }
        public static BaseResponse<TResponse> Failure(int StatusCode = -1, string message = "Create Failed!!")
        {
            return new BaseResponse<TResponse>()
            {
                Success = false,
                StatusCode = StatusCode,
                Message = message
             
            };
        }
        public static BaseResponse<TResponse> Ok(TResponse data = default(TResponse),int StatusCode = 0,string message = "Success")
        {
            return new BaseResponse<TResponse>()
            {
                Success = true,
                StatusCode = StatusCode,
                Data = data,
                Message = message
            };
        }
    }
}
