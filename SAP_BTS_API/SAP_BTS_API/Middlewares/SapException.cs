using System.Net;

public class SapException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public object ErrorData { get; set; }

    public SapException(HttpStatusCode statusCode, string message, object errorData) : base(message)
    {
        StatusCode = statusCode;
        ErrorData = errorData;
    }
}