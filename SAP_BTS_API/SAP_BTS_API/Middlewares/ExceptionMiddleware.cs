using SAP_BTS_API.Domain.common;
using System.Net;

namespace SAP_BTS_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            object? errorData = null;

            if (ex is SapException sapEx)
            {
                statusCode = sapEx.StatusCode;
                errorData = sapEx.ErrorData; // Bây giờ nó là Object { Code, Message }, không phải String
            }

            context.Response.StatusCode = (int)statusCode;

            // Dùng BaseResponse<object> để ép kiểu Data linh hoạt
            var response = new BaseResponse<object>
            {
                isSuccess = false,
                message = ex.Message,
                statusCode = statusCode,
                Data = errorData
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}